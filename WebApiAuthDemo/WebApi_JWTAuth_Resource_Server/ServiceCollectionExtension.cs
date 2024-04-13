using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace WebApi_JWTAuth_Resource_Server;
public static partial class ServiceCollectionExtension
{
    public static IServiceCollection AddAuthServices(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtTokenOptions = configuration.GetSection(JwtTokenOptions.JwtToken).Get<JwtTokenOptions>();
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        // Adding Jwt Bearer token verification, the token was generated in the WebApi_JWTAuthentication project
        // Use same options as the one used in the WebApi_JWTAuthentication project
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidAudience = jwtTokenOptions!.Audience,
                ValidIssuer = jwtTokenOptions.Issuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenOptions!.Secret)),
                ClockSkew = TimeSpan.Zero
            };
        });
        services.AddAuthorization(options => { options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build(); });
        return services;
    }
}
