using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WebApiJWTAuthentication.Services
{
    public interface IJwtService
    {
        public JwtSecurityToken GenerateAccessToken(List<Claim> authClaims);
        public string GenerateRefreshToken();
        public bool ValidateAccessToken(string token);
        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
    }
}
