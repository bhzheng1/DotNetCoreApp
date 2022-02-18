using Microsoft.AspNetCore.Authentication.Certificate;
using WebApi.CertificateAuthentication;
using Microsoft.AspNetCore.Server.Kestrel.Https;

var builder = WebApplication.CreateBuilder(args);

var certConf = new CertConfig();
builder.Configuration.GetSection(CertConfig.Name).Bind(certConf);

builder.WebHost.UseKestrel(op =>
    {
        op.ConfigureHttpsDefaults(o => { 
            o.ClientCertificateMode = ClientCertificateMode.RequireCertificate;
            o.ServerCertificate = CertificateHelper.GetServiceCertificate(certConf.CertSerialValue, certConf.CertEnv);
        });
    });
// Add services to the container.
builder.Services.Configure<CertConfig>(builder.Configuration.GetSection(CertConfig.Name));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddTransient<CertificateValidation>();

builder.Services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme).AddCertificate(
    options =>
    {
        options.AllowedCertificateTypes = CertificateTypes.All;
        options.Events = new CertificateAuthenticationEvents()
        {
            OnCertificateValidated = context =>
            {
                var validationService = context.HttpContext.RequestServices.GetService<CertificateValidation>();
                if (validationService != null & validationService.ValidateCertificate(context.ClientCertificate))
                {
                    context.Success();
                }
                else
                {
                    context.Fail("Invalid certificate");
                }
                return Task.CompletedTask;
            },
            OnAuthenticationFailed = context =>
            {
                context.Fail("Invalid certificate");
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
