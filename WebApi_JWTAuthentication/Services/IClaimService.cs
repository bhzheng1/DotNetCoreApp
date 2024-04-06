using System.Security.Claims;
using WebApiJWTAuthentication.DataAccess.Entities;

namespace WebApiJWTAuthentication.Services
{
    public interface IClaimService
    {
        Task<List<Claim>> GetValidClaims(ApplicationUser user);
    }
}