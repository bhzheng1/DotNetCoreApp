using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebApiJWTAuthentication.Models;

namespace WebApiJWTAuthentication.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtTokenOptions _jwtTokenOptions;
        public JwtService(IOptions<JwtTokenOptions> options)
        {
            _jwtTokenOptions = options.Value;
        }
        public JwtSecurityToken GenerateAccessToken(List<Claim> authClaims)
        {
            var authSecretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenOptions.Secret));
            var expires = _jwtTokenOptions.Expires;
            var issuer = _jwtTokenOptions.Issuer;
            var aud = _jwtTokenOptions.Audience;
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: aud,
                expires: DateTime.UtcNow.AddMinutes(expires),
                notBefore: DateTime.UtcNow,
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSecretKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        public bool ValidateAccessToken(string token)
        {
            var issuer = _jwtTokenOptions.Issuer;
            var audience = _jwtTokenOptions.Audience;
            var key = Encoding.UTF8.GetBytes(_jwtTokenOptions.Secret);

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var _ = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;

        }
        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenOptions.Secret)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;

        }
    }
}
