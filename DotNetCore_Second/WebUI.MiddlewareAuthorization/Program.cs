using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using WebUI.MiddlewareAuthorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IAuthorizationHandler, CustomAuthorizationHandler>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => {
    options.LoginPath = new PathString("/Account/Login");//没登录跳到这个路径
    options.AccessDeniedPath = new PathString("/Account/AccessDenied");//没权限跳到这个路径
});
builder.Services.AddAuthorization(options => {
    options.AddPolicy("customPolicy", policy => {
        policy.AddRequirements(new CustomAuthorizationRequirement("Policy01"));
    });
});

builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();

app.UseAuthentication();//检测用户是否登录
app.UseAuthorization(); //授权，检测有没有权限，是否能够访问功能

app.MapRazorPages();
app.MapDefaultControllerRoute();

app.Run();
