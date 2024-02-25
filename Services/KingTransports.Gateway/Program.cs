using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel((context, serverOptions) =>
{
    serverOptions.Listen(IPAddress.Any, 5050);
});

var configuration = builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("ocelot.json", false).Build();

//Ocelot
builder.Services.AddOcelot(configuration).AddConsul();

builder.Logging.AddConsole();
var app = builder.Build();

app.UseOcelot().Wait();

app.Run();