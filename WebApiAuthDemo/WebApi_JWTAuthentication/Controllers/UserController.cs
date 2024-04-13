using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApiJWTAuthentication.DataAccess.Entities;
using WebApiJWTAuthentication.Models;
using WebApiJWTAuthentication.Services;

namespace WebApiJWTAuthentication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IClaimService _claimOperation;

        public UserController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IClaimService claimOperation
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _claimOperation = claimOperation;
        }

        [HttpGet]
        [Authorize]
        [Route("{userId}/profile")]
        public async Task<IActionResult> GetUserProfile(Guid userId)
        {
            var loginUserIdentity = await _userManager.FindByNameAsync(User.Identity.Name);
            var loginUserRoles = await _userManager.GetRolesAsync(loginUserIdentity);
            if (!loginUserRoles.Contains("Admin") && loginUserIdentity.Id != userId)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new ApiResponse
                {
                    Status = StatusCodes.Status401Unauthorized.ToString(),
                    Message = "Unauthorized"
                });
            }

            var userExist = await _userManager.FindByIdAsync(userId.ToString());
            if (userExist == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ApiResponse
                {
                    Status = StatusCodes.Status404NotFound.ToString(),
                    Message = "User doesn't exist!"
                });
            }
            var claims = await _claimOperation.GetValidClaims(userExist);

            return Ok(
                new
                {
                    user = userExist,
                    userRoles = await _userManager.GetRolesAsync(userExist),
                    userClaims = claims,
                });
        }

        [HttpPatch]
        [Authorize(Roles = "Admin")]
        [Route("{userId}/add-role")]
        public async Task<IActionResult> AddUserToRole(string userId, [FromBody] IdentityRole role)
        {

            var userExist = await _userManager.FindByIdAsync(userId);
            if (userExist == null)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden.ToString(),
                    Message = "User doesn't exists!"
                });
            }

            var roleExist = await _roleManager.FindByIdAsync(role.Id);
            if (roleExist == null)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden.ToString(),
                    Message = "Role doesn't exists!"
                });
            }

            var result = await _userManager.AddToRoleAsync(userExist, roleExist.Name);
            if (result.Succeeded)
            {
                return StatusCode(StatusCodes.Status201Created, new ApiResponse
                {
                    Status = StatusCodes.Status201Created.ToString(),
                    Message = "Added user to role successfully!"
                });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse
                {
                    Status = StatusCodes.Status500InternalServerError.ToString(),
                    Message = "Failed to add user to role!"
                });
            }
        }

    }
}
