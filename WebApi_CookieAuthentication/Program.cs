using Microsoft.AspNetCore.Authentication.Cookies;
using WebApi_CookieAuthentication.Records;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    }).AddCookie
    (
        options =>
        {
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
            options.SlidingExpiration = true;
            options.AccessDeniedPath = "/Forbidden/";

            // By default, cookie authentication redirects the user to the login URL if authentication failed.
            // Hence, we’re setting the delegate function
            options.Events.OnRedirectToLogin = (context) =>
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Task.CompletedTask;
            };
            options.Cookie.HttpOnly = true;
            // Only use this when the sites are on different domains
            options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
        }
    );
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(AuthPolicy.MustBeAdmin, policy => policy.RequireClaim("userType", "admin"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

