using KingTransports.FleetService.Entities;
using Microsoft.EntityFrameworkCore;

namespace KingTransports.FleetService.Data;

public class DbInitializer
{
    public static void InitDb(WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        SeedData(scope.ServiceProvider.GetService<FleetDbContext>());
    }

    private static void SeedData(FleetDbContext context)
    {
        context.Database.Migrate();

        if (context.Vehicles.Any())
        {
            Console.WriteLine("Already have data - no need to seed");
            return;
        }

        var vehicles = new List<Vehicle>()
        {
           new Vehicle
            {
                VehicleId = Guid.Parse("2aa9a169-cd71-47c4-a1a2-0c79fd988b22"),
                Make = "Volvo Buses",
                Model = "7700",
                VehicleType = VehicleType.Bus,
                MaintanceKm = 30000,
                MaintanceMonths = 6
            },
            new Vehicle
            {
                VehicleId = Guid.Parse("65202de8-3976-45d0-abbf-528b09d93ecc"),
                Make = "Hyundai Motor Company",
                Model = "Aero City",
                VehicleType = VehicleType.Bus,
                MaintanceKm = 45000,
                MaintanceMonths = 3
            },
        };

        context.AddRange(vehicles);

        var fleetVehicles = new List<FleetVehicle>()
        {
            new FleetVehicle
            {
                FleetVehicleId = Guid.Parse("b4142c35-94ed-4150-832b-e90fb68a13d5"),
                VehicleId = Guid.Parse("2aa9a169-cd71-47c4-a1a2-0c79fd988b22"),
                VehicleVin = "4Y1SL65848Z411439",
                InServiceFrom = DateTime.UtcNow.AddYears(-3),
                VehicleOperabilityStatus = FleetVehicleOperabilityStatus.Operable
            },
              new FleetVehicle
            {
                FleetVehicleId = Guid.Parse("25781252-4e21-450d-973b-22c2738df362"),
                VehicleId = Guid.Parse("65202de8-3976-45d0-abbf-528b09d93ecc"),
                VehicleVin = "5Y1SL65848Z411438",
                InServiceFrom = DateTime.UtcNow.AddYears(-5),
                VehicleOperabilityStatus = FleetVehicleOperabilityStatus.Operable
            }
        };

        context.AddRange(fleetVehicles);

        var metrics = new List<FleetVehicleMetric>();

        for(int i = 1; i < 20; i++)
        {
            metrics.Add(new FleetVehicleMetric()
            {
                FleetVehicleMetricId = Guid.NewGuid(),
                FleetVehicleId = Guid.Parse("b4142c35-94ed-4150-832b-e90fb68a13d5"),
                LoggedAt = DateTime.UtcNow.AddDays(-1 * i),
                OdometerKm = (decimal)(i * 6.5 * 1000)
            });

            metrics.Add(new FleetVehicleMetric()
            {
                FleetVehicleMetricId = Guid.NewGuid(),
                FleetVehicleId = Guid.Parse("25781252-4e21-450d-973b-22c2738df362"),
                LoggedAt = DateTime.UtcNow.AddDays(-1 * i),
                OdometerKm = (decimal)(i * 7.5 * 1000)
            });
        }

        context.AddRange(metrics);

        var maintanceLogs = new List<FleetVehicleMaintanceLog>();

        for (int i = 1; i < 20; i++)
        {
            maintanceLogs.Add(new FleetVehicleMaintanceLog()
            {
                FleetVehicleId = Guid.Parse("b4142c35-94ed-4150-832b-e90fb68a13d5"),
                FleetVehicleMaintanceLogId = Guid.NewGuid(),
                MaintainceType = i % 5 == 0 ? FleetVehicleMaintainceReason.UnexpectedRepair : FleetVehicleMaintainceReason.RegularMaintance,
                MaintanceStartedAt = DateTime.UtcNow.AddDays(-10 * 1),
                MaintanceFinishedAt = DateTime.UtcNow.AddDays(-5 * 1),
                MaintanceCost = new Random().Next(1000, 10000),
                OdometerKm = (decimal)(i * 6.5 * 1000),
                NewVehicleOperabilityStatus = FleetVehicleOperabilityStatus.Operable
            });

            maintanceLogs.Add(new FleetVehicleMaintanceLog()
            {
                FleetVehicleId = Guid.Parse("25781252-4e21-450d-973b-22c2738df362"),
                FleetVehicleMaintanceLogId = Guid.NewGuid(),
                MaintainceType = i % 5 == 0 ? FleetVehicleMaintainceReason.UnexpectedRepair : FleetVehicleMaintainceReason.RegularMaintance,
                MaintanceStartedAt = DateTime.UtcNow.AddDays(-10 * 1),
                MaintanceFinishedAt = DateTime.UtcNow.AddDays(-5 * 1),
                MaintanceCost = new Random().Next(1000, 10000),
                OdometerKm = (decimal)(i * 6.5 * 1000),
                NewVehicleOperabilityStatus = FleetVehicleOperabilityStatus.Operable
            });
        }

        context.AddRange(maintanceLogs);
        context.SaveChanges();
    }
}
