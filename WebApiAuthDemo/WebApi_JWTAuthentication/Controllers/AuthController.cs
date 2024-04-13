using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using WebApiJWTAuthentication.DataAccess.Entities;
using WebApiJWTAuthentication.Models;
using WebApiJWTAuthentication.Services;

namespace WebApiJWTAuthentication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IClaimService _claimOperation;
        private readonly IJwtService _jwtService;
        private readonly JwtTokenOptions _jwtTokenOptions;
        public AuthController(
            UserManager<ApplicationUser> userManager,
            IClaimService claimOperation,
            IJwtService jwtService,
            IOptions<JwtTokenOptions> options
            )
        {
            _userManager = userManager;
            _claimOperation = claimOperation;
            _jwtService = jwtService;
            _jwtTokenOptions = options.Value;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register-user")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUser registerUser)
        {
            var userExist = await _userManager.FindByEmailAsync(registerUser.Email);
            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden.ToString(),
                    Message = "User already exists!"
                });
            };

            var user = new ApplicationUser()
            {
                Email = registerUser.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerUser.Username
            };
            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (result.Succeeded)
            {
                return StatusCode(StatusCodes.Status201Created, new ApiResponse
                {
                    Status = StatusCodes.Status201Created.ToString(),
                    Message = "User created successfully!"
                });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse
                {
                    Status = StatusCodes.Status500InternalServerError.ToString(),
                    Message = "Failed to create user!"
                });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login-user")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUser loginUser)
        {
            var userExist = await _userManager.FindByNameAsync(loginUser.Username);

            if (userExist != null && await _userManager.CheckPasswordAsync(userExist, loginUser.Password))
            {
                var authClaims = await _claimOperation.GetValidClaims(userExist);
                var jwtToken = _jwtService.GenerateAccessToken(authClaims);
                var refreshToken = _jwtService.GenerateRefreshToken();
                userExist.RefreshToken = refreshToken;
                userExist.RefreshTokenExpiresOn = DateTimeOffset.UtcNow.AddDays(_jwtTokenOptions.RefreshExpires);
                await _userManager.UpdateAsync(userExist);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    refreshToken,
                    expiration = jwtToken.ValidTo
                });
            }
            return Unauthorized();
        }


    }
}
