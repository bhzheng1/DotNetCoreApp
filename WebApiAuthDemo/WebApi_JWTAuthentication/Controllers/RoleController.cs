using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApiJWTAuthentication.DataAccess.Entities;
using WebApiJWTAuthentication.Models;

namespace WebApiJWTAuthentication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        public RoleController(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpPatch]
        [Authorize(Roles = "Admin")]
        [Route("{roleId}/add-claims")]
        public async Task<IActionResult> AddRoleClaims(string roleId, [FromBody] IList<ClaimModel> claims)
        {

            var roleExist = await _roleManager.FindByIdAsync(roleId);
            if (roleExist == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ApiResponse
                {
                    Status = StatusCodes.Status404NotFound.ToString(),
                    Message = "Role doesn't exist!"
                });
            }

            foreach (var item in claims)
            {
                await _roleManager.AddClaimAsync(roleExist, new Claim(item.Claimname, item.Claimvalue));
            }

            return StatusCode(StatusCodes.Status201Created, new ApiResponse
            {
                Status = StatusCodes.Status201Created.ToString(),
                Message = "Claims created successfully!"
            });
        }
    }
}
