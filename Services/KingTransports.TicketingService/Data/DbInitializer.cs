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
                StationId = Guid.NewGuid(),
                StationName = "Zagreb - Glavni kolodvor",
                StationType = StationType.BusStop

            },
            new Station
            {
                StationId = Guid.NewGuid(),
                StationName = "Velika gorica",
                StationType = StationType.BusStop
            },
            new Station
            {
                StationId = Guid.NewGuid(),
                StationName = "Dugave",
                StationType = StationType.BusStop
            },
            new Station
            {
               StationId = Guid.NewGuid(),
                StationName = "Travno",
                StationType = StationType.BusStop
            },
            new Station
            {
                StationId = Guid.NewGuid(),
                StationName = "Kajzerica",
                StationType = StationType.BusStop
            },
            new Station
            {
                StationId = Guid.NewGuid(),
                StationName = "Veliko polje",
                StationType = StationType.BusStop
            },
        };

        context.AddRange(stations);

        var routes = new List<Route>();

        for(int i = 1; i < stations.Count - 1; i++)
        {
            routes.Add(new Route
            {
                RouteId = Guid.NewGuid(),
                StationFromId = stations[0].StationId,
                StationToId = stations[i].StationId,
                DistanceKm = i * 2
            });
        };

        context.AddRange(routes);

        var tickets = new List<Ticket>()
        {
            new Ticket
            {
                TicketId = Guid.Parse("81ed6ad5-d618-4458-b163-50b17031e8b6"),
                RouteId =  routes[0].RouteId,
                ValidFrom = DateTime.UtcNow,
                ValidTo = DateTime.UtcNow.AddDays(1),
                IssuedAt = DateTime.UtcNow,
                Price = 10
            }
        };

        for (int i = 0; i < 30; i++)
        {
            var route = routes[new Random().Next(routes.Count - 1)];
            tickets.Add(new Ticket
            {
                TicketId = Guid.NewGuid(),
                RouteId = route.RouteId,
                ValidFrom = DateTime.UtcNow,
                ValidTo = DateTime.UtcNow.AddDays(1),
                IssuedAt = DateTime.UtcNow,
                Price = (decimal)(route.DistanceKm * 0.5)
            });
        }

        context.AddRange(tickets);
        context.SaveChanges();
    }
}
