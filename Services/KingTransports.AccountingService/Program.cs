using IdentityServer4.AccessTokenValidation;
using KingTransports.Common.Filters;
using KingTransports.TicketingService.Data;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using KingTransports.Common.Discovery;
using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using KingTransports.TicketingService.Repositories;
using KingTransports.TicketingService.Services;
using KingTransports.AccountingService.Consumers;
using KingTransports.AccountingService.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel((context, serverOptions) =>
{
    serverOptions.Listen(IPAddress.Any, 7003);
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
builder.Services.AddDbContext<AccountingDbContext>(option =>
{
    var conn = builder.Configuration.GetConnectionString("DefaultConnection");
    option.UseNpgsql(conn);
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddMassTransit(x =>
{
    x.AddConsumers(Assembly.GetExecutingAssembly());

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
            options.ApiName = "accounting";
            options.RequireHttpsMetadata = false;
        });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("accounting.read", policy =>
    {
        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireScope("accounting.read");
    });
});

builder.Services.AddHealthChecks();

// Add services to the container

builder.Services.AddTransient<ITransactionRepository, TransactionRepository>();
builder.Services.AddTransient<ITransactionService, TransactionService>();

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

