using Microsoft.EntityFrameworkCore;
using MassTransit;
using KingTransports.FleetService.Entities;

namespace KingTransports.FleetService.Data
{
    public class FleetDbContext : DbContext
    {
        public FleetDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<FleetVehicle> FleetVehicles { get; set; }
        public DbSet<FleetVehicleMaintanceLog> FleetVehicleMaintanceLog { get; set; }
        public DbSet<FleetVehicleMetric> FleetVehicleMetrics { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // add in memory outbox https://masstransit.io/documentation/patterns/in-memory-outbox
            builder.AddInboxStateEntity();
            builder.AddOutboxMessageEntity();
            builder.AddOutboxStateEntity();
        }
    }
}