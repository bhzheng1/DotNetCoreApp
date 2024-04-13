using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using System.Text.Json;
using WebApi_Application.DataAccess;
using WebApi_Application.Extensions;
using WebApi_Application.Models;
using Module_WorldDemo;
using MediatRDemo;

var builder = WebApplication.CreateBuilder(args);

/*
Setting Host environment variable to override values in your appsettings.json
https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-8.0

Prefix your env var with ASPNETCORE_
Use double underscore to separate nested fields __
So to set the WorldDataBase in our ConnectionStrings section we would run or build the application with the variable:
ASPNETCORE_ConnectionStrings__WorldDataBase=my-string
*/

builder.Configuration.AddEnvironmentVariables();

foreach (var c in builder.Configuration.AsEnumerable())
{
    Console.WriteLine(c.Key + " = " + c.Value);
}

ConfigurationManager configuration = builder.Configuration;


#region Logger configuration

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

// add the serilog to the logging pipeline
// builder.Logging.ClearProviders();
// builder.Logging.AddSerilog(logger);

// use serilog as the only logging provider
builder.Host.UseSerilog(logger);

#endregion

#region Microsoft sql server
var appDatabaseStr = configuration.GetConnectionString("AppDataBase");
// For sql server database
builder.Services.AddDataBaseAccess(opts => opts.UseSqlServer(appDatabaseStr));

// For InMemoryDatabase
//builder.Services.AddDataBaseAccess(opts => opts.UseInMemoryDatabase("test"));

//for identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<AppDataBaseContext>()
                .AddDefaultTokenProviders();
#endregion




// Adding Authentication
builder.Services.AddOptions<EmailOptions>().Bind(configuration.GetSection(EmailOptions.EmailConfig));
builder.Services.AddOptions<JwtTokenOptions>().Bind(configuration.GetSection(JwtTokenOptions.JwtToken));
var jwtTokenOptions = configuration.GetSection(JwtTokenOptions.JwtToken).Get<JwtTokenOptions>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer
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



#region mudule sample: Module_WorldDemo

var connectionStr = configuration.GetConnectionString("WorldDataBase");
// will automatically add the controllers and services
builder.Services.AddWorldDataBaseAccess(opts => opts.UseNpgsql(connectionStr));
//builder.Services.AddClDataBaseAccess(opts => opts.UseInMemoryDatabase("WorldDataBase"));
builder.Services.AddWorldModuleDependencies();

#endregion
// add data access module


// add the mediatR module
builder.Services.AddMediatRModuleDI();


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "all", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});


builder.Services.AddControllers().AddJsonOptions(
    options =>
    {
        options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

// make the routing lowercase
builder.Services.AddRouting(options => options.LowercaseUrls = true);


if (builder.Environment.IsDevelopment())
{
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = JwtBearerDefaults.AuthenticationScheme
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
        });
    });
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c =>
    {
        c.RouteTemplate = "api/swagger/{documentname}/swagger.json";
    });
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/api/swagger/v1/swagger.json", "My Cool API V1");
        c.RoutePrefix = "api/swagger";
    });
    app.UseExceptionHandler("/error-development");
}
else
{

    app.UseExceptionHandler("/error");
}

app.UseHsts();


await app.EnsureAppDatabaseUpdated();

# region mudule sample: Module_WorldDemo
await app.EnsureModuleDatabaseUpdated();
# endregion
//
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("all");
app.MapControllers();

app.Run();
