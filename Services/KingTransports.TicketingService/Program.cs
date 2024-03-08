using IdentityServer4.AccessTokenValidation;
using KingTransports.Common.Filters;
using KingTransports.TicketingService.Data;
using KingTransports.TicketingService.Repositories;
using KingTransports.TicketingService.Services;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using KingTransports.Common.Discovery;
using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel((context, serverOptions) =>
{
    serverOptions.Listen(IPAddress.Any, 7001);
});

// Add services to the container.
builder.Services.AddControllers(config =>
{
    config.Filters.Add(typeof(ValidationFilter));
    config.Filters.Add(typeof(ErrorHandlingFilter));
});

builder.Services.AddLogging();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<TicketDbContext>(option =>
{
    var conn = builder.Configuration.GetConnectionString("DefaultConnection");
    option.UseNpgsql(conn);
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Configure RabbitMq
builder.Services.AddMassTransit(x =>
{
    x.AddEntityFrameworkOutbox<TicketDbContext>(o =>
    {
        o.QueryDelay = TimeSpan.FromSeconds(10);

        o.UsePostgres();
        o.UseBusOutbox();
    });

    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("ticketing", false));

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

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
});

builder.Services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
        .AddIdentityServerAuthentication(options =>
        {
            options.Authority = "http://localhost:5050/auth";
            options.ApiName = "ticketing";
            options.RequireHttpsMetadata = false;
        });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ticket.issue", policy =>
    {
        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireScope("ticket.issue");
    });

    options.AddPolicy("ticket.validate", policy =>
    {
        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireScope("ticket.validate");
    });
});

builder.Services.AddHealthChecks();

// Add services to the container

builder.Services.AddTransient<IRouteRepository, RouteRepository>();
builder.Services.AddTransient<ITicketRepository, TicketRepository>();
builder.Services.AddTransient<ITicketService, TicketService>();

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

