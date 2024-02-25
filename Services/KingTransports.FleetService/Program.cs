using IdentityServer4.AccessTokenValidation;
using KingTransports.Common.Filters;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using KingTransports.Common.Discovery;
using System.Net;
using KingTransports.FleetService.Services;
using KingTransports.FleetService.Data;
using KingTransports.FleetService.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel((context, serverOptions) =>
{
    serverOptions.Listen(IPAddress.Any, 7002);
});

// Add services to the container.
builder.Services.AddControllers(config =>
{
    config.Filters.Add(typeof(ErrorHandlingFilter));
});

builder.Services.AddLogging();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<FleetDbContext>(option =>
{
    var conn = builder.Configuration.GetConnectionString("DefaultConnection");
    option.UseNpgsql(conn);
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Configure RabbitMq
builder.Services.AddMassTransit(x =>
{
    // add in memory outbox
    x.AddEntityFrameworkOutbox<FleetDbContext>(o =>
    {
        o.QueryDelay = TimeSpan.FromSeconds(10);

        o.UsePostgres();
        o.UseBusOutbox();
    });

    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("ticket", false));

    x.UsingRabbitMq((context, cfg) =>
    {

        cfg.Host(builder.Configuration["RabbitMq:Host"], "/", host =>
        {
            host.Username(builder.Configuration.GetValue("RabbitMq:Username", "guest"));
            host.Password(builder.Configuration.GetValue("RabbitMq:Password", "guest"));
        });
        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
        .AddIdentityServerAuthentication(options =>
        {
            options.Authority = "http://localhost:5050/auth";
            options.ApiName = "fleet";
            options.RequireHttpsMetadata = false;
        });


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("fleet.create", policy =>
    {
        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireScope("fleet.create");
    });

    options.AddPolicy("fleet.read", policy =>
    {
        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireScope("fleet.read");
    });
});

builder.Services.AddHealthChecks();

// Add services to the container

builder.Services.AddTransient<IVehicleRepository, VehicleRepository>();
builder.Services.AddTransient<IFleetVehicleRepository, FleetVehicleRepository>();
builder.Services.AddTransient<IFleetVehicleService, FleetVehicleService>();

builder.Services.RegisterConsulServices(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapHealthChecks("/health");
app.MapControllers();

try
{
    DbInitializer.InitDb(app);
}
catch (Exception e)
{

    Console.WriteLine(e);
}

app.Run();

