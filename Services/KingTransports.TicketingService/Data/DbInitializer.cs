using KingTransports.TicketingService.Entities;
using Microsoft.EntityFrameworkCore;
using Route = KingTransports.TicketingService.Entities.Route;

namespace KingTransports.TicketingService.Data;

public class DbInitializer
{
    public static void InitDb(WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        SeedData(scope.ServiceProvider.GetService<TicketDbContext>());
    }

    private static void SeedData(TicketDbContext context)
    {
        context.Database.Migrate();

        if (context.Stations.Any())
        {
            Console.WriteLine("Already have data - no need to seed");
            return;
        }

        //context.Tickets.ExecuteDelete();
        //context.Routes.ExecuteDelete();
        //context.Stations.ExecuteDelete();

        var stations = new List<Station>()
        {
           new Station
            {
                StationId = Guid.Parse("dbd6dbac-c65b-4a98-844c-eda5357ee2a7"),
                StationName = "Velika Gorica",
                StationType = StationType.BusStop

            },
           new Station
            {
                StationId = Guid.Parse("f7f668fa-7f59-4820-ada0-b5575caa2233"),
                StationName = "Zagreb - Glavni kolodvor",
                StationType = StationType.BusStop

            },
        };

        context.AddRange(stations);

        var routes = new List<Route>()
        {
            new Route
            {
                RouteId = Guid.Parse("97dcd61c-0714-4d52-91e2-1c6b3d05c676"),
                StationFromId = Guid.Parse("dbd6dbac-c65b-4a98-844c-eda5357ee2a7"),
                StationToId = Guid.Parse("f7f668fa-7f59-4820-ada0-b5575caa2233"),
                DistanceKm = 12
            }
        };

        context.AddRange(routes);

        var tickets = new List<Ticket>()
        {
            new Ticket
            {
                TicketId = Guid.Parse("81ed6ad5-d618-4458-b163-50b17031e8b6"),
                RouteId =  Guid.Parse("97dcd61c-0714-4d52-91e2-1c6b3d05c676"),
                ValidFrom = DateTime.UtcNow,
                ValidTo = DateTime.UtcNow.AddDays(1),
                IssuedAt = DateTime.UtcNow,
                Price = 10
            }
        };

        context.AddRange(tickets);


        context.SaveChanges();
    }
}
