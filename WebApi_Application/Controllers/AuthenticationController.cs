using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi_Application.Models;

namespace WebApi_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUser registerUser, string role)
        {
            var userExist = await _userManager.FindByEmailAsync(registerUser.Email);
            if (userExist != null) return StatusCode(StatusCodes.Status403Forbidden, new Response { Status = "Error", Message = "User already exists!" });
            if (!await _roleManager.RoleExistsAsync(role)) return StatusCode(StatusCodes.Status403Forbidden, new Response { Status = "Error", Message = "Role doesn't exists!" });

            IdentityUser user = new()
            {
                Email = registerUser.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerUser.UserName
            };
            var result = await _userManager.CreateAsync(user, registerUser.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User Created Failed!" });

            }
            await _userManager.AddToRoleAsync(user, role);

            return StatusCode(StatusCodes.Status201Created, new Response { Status = "Success", Message = "User Created Successfully!" });
        }
    }
}

