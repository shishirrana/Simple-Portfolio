using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using com.portfolio.website.Data;
using Microsoft.Extensions.Options;
using com.portfolio.website.Constants;
using com.portfolio.website.Filters;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

/*builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(Constant.ROLE, policy => policy.RequireRole(Constant.ROLE));
        
});*/

builder.Services.AddDbContext<comportfoliowebsiteContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("comportfoliowebsiteContext") ?? throw new InvalidOperationException("Connection string 'comportfoliowebsiteContext' not found.")));

//Authentication and Authorization using Policy, Roles and Clains

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(config =>
    {
        config.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        config.LoginPath = "/User/Login"; //path to redirect user login path
        config.AccessDeniedPath = "/Home/AccessDenied";
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("admin", policy => policy.RequireRole("admin"));
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();
});


builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential= true;
}
    );

builder.Services.AddMvc((options) =>
    options.Filters.Add(typeof(MyAuthorizeAttribute))
);


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
