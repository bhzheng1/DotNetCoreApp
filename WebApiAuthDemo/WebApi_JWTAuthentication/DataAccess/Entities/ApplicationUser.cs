using Microsoft.AspNetCore.Identity;

namespace WebApiJWTAuthentication.DataAccess.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            RefreshTokens = new List<RefreshToken>();
        }
        public string? RefreshToken { get; set; }
        public DateTimeOffset? RefreshTokenExpiresOn { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
