using KingTransports.Auth;
using KingTransports.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using KingTransports.Common.Discovery;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

if (builder.Environment.IsDevelopment())
{
    builder.WebHost.ConfigureKestrel((context, serverOptions) =>
    {
        serverOptions.Listen(IPAddress.Any, 5005);
    });
}

string connectionString;
if (builder.Environment.IsDevelopment())
{
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
}
else
{
    var server = Environment.GetEnvironmentVariable("PGSQL_SERVER");
    var port = Environment.GetEnvironmentVariable("PGSQL_PORT");
    var user = Environment.GetEnvironmentVariable("PGSQL_USER");
    var password = Environment.GetEnvironmentVariable("PGSQL_PASSWORD");
    connectionString = $"Server={server};Port={port};Database=auth;User Id={user};Password={password};";
}

// AWS ALB reverse proxy schema fix
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
}

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

builder.Services.AddIdentityServer()
 .AddInMemoryClients(AuthConfig.Clients)
 .AddInMemoryIdentityResources(AuthConfig.IdentityResources)
 .AddInMemoryApiResources(AuthConfig.ApiResources)
 .AddInMemoryApiScopes(AuthConfig.ApiScopes)
 .AddDeveloperSigningCredential()
 .AddAspNetIdentity<IdentityUser>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
});

builder.Services.AddAuthentication();
builder.Services.AddHealthChecks();

if (builder.Environment.IsDevelopment())
{
    builder.Services.RegisterConsulServices(builder.Configuration);
}

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DbInitializer.SeedUsers(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}

app.UsePathBase(new PathString("/auth"));

// Allowing only login, logout for demo and connect
app.Use(async (context, next) =>
{
    var allowed = new string[] { "/", "/health", "/lib", "/css", "/js", "/KingTransports.styles", "/Identity/lib", "/Identity/Account/Login", "/Identity/Account/Logout", "/connect" };
    if (context.Request.Path.Value == "" || allowed.Any(x => context.Request.Path.Value.ToLower().StartsWith(x.ToLower())))
    {
        await next();
        return;
    }
    context.Response.StatusCode = 404; //Not found
    return;
});
app.UseStaticFiles();

app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();
app.MapHealthChecks("/health");
app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
