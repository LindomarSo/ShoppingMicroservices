using Duende.IdentityServer.Services;
using Geek.IdentityServer.Configuration;
using Geek.IdentityServer.Initializer;
using Geek.IdentityServer.Model;
using Geek.IdentityServer.Model.Context;
using Geek.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MySQLContext>(options =>
{
    var connection = builder.Configuration["MySqlConnection:MySqlConnectionString"];
    options.UseMySql(connection, new MySqlServerVersion(new Version()));
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
    .AddEntityFrameworkStores<MySQLContext>()
    .AddDefaultTokenProviders();

builder.Services.AddIdentityServer(options =>
{
   options.Events.RaiseErrorEvents = true;
   options.Events.RaiseInformationEvents = true;
   options.Events.RaiseFailureEvents = true;
   options.Events.RaiseSuccessEvents = true;
   options.EmitStaticAudienceClaim = true;
}).AddInMemoryIdentityResources(IdentityConfiguration.IdentityResources)
 .AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
 .AddInMemoryClients(IdentityConfiguration.Clients)
 .AddAspNetIdentity<ApplicationUser>()
 .AddDeveloperSigningCredential();

builder.Services.AddRazorPages();

builder.Services.AddScoped<IDbInitiliazer, DbInitiliazer>();
builder.Services.AddScoped<IProfileService, ProfileService>();

var app = builder.Build();

var initialize = app.Services.CreateScope().ServiceProvider.GetService<IDbInitiliazer>();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();
app.MapRazorPages();

initialize?.Initialize();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
