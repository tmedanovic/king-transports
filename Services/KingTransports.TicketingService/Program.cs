using IdentityServer4.AccessTokenValidation;
using KingTransports.Common.Filters;
using KingTransports.TicketingService.Data;
using KingTransports.TicketingService.Repositories;
using KingTransports.TicketingService.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using KingTransports.Common.Discovery;
using System.Net;
using Amazon.Runtime;
using Amazon.Util;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

if (builder.Environment.IsDevelopment())
{
    builder.WebHost.ConfigureKestrel((context, serverOptions) =>
    {
        serverOptions.Listen(IPAddress.Any, 7001);
    });
}

// Add services to the container.
builder.Services.AddControllers(config =>
{
    config.SuppressAsyncSuffixInActionNames = false;

    config.Filters.Add(typeof(ValidationFilter));
    config.Filters.Add(typeof(ErrorHandlingFilter));
});

builder.Services.AddLogging();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

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
    connectionString = $"Server={server};Port={port};Database=ticketing;User Id={user};Password={password};";
}

builder.Services.AddDbContext<TicketDbContext>(option =>
{
    option.UseNpgsql(connectionString);
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddMassTransit(x =>
{
    x.AddEntityFrameworkOutbox<TicketDbContext>(o =>
    {
        o.QueryDelay = TimeSpan.FromSeconds(10);

        o.UsePostgres();
        o.UseBusOutbox();
    });

    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("ticketing", false));

    if (builder.Environment.IsDevelopment())
    {
        x.UsingRabbitMq((context, cfg) =>
        {

            cfg.Host(builder.Configuration["RabbitMq:Host"], "/", host =>
            {
                host.Username(builder.Configuration.GetValue("RabbitMq:Username", "guest"));
                host.Password(builder.Configuration.GetValue("RabbitMq:Password", "guest"));
            });
            cfg.ConfigureEndpoints(context);
        });
    }
    else
    {
        x.UsingAmazonSqs((context, cfg) =>
        {
            cfg.Host("us-east-1", h =>
            {
                var credentials = new ECSTaskCredentials();
                h.Credentials(credentials);
            });
        });
    }
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
            if (builder.Environment.IsDevelopment())
            {
                options.Authority = builder.Configuration.GetValue("IdentityServer:Authority", "");
            }
            else
            {
                var domain = Environment.GetEnvironmentVariable("ALB_DOMAIN");
                options.Authority = string.Format("https://{0}/auth", domain);
            }
            options.ApiName = builder.Configuration.GetValue("IdentityServer:ApiName", "");
            options.RequireHttpsMetadata = false;
        });

builder.Services.AddHealthChecks();

// Add services to the container

builder.Services.AddTransient<IRouteRepository, RouteRepository>();
builder.Services.AddTransient<ITicketRepository, TicketRepository>();
builder.Services.AddTransient<IRouteService, RouteService>();
builder.Services.AddTransient<ITicketService, TicketService>();

if (builder.Environment.IsDevelopment())
{
    builder.Services.RegisterConsulServices(builder.Configuration);
}

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

if (!builder.Environment.IsDevelopment())
{
    app.UsePathBase(new PathString("/ticketing"));
}

app.UseRouting();
app.MapControllers();
app.MapHealthChecks("/health");

try
{
    DbInitializer.InitDb(app);
}
catch (Exception e)
{

    Console.WriteLine(e);
}

app.Run();

