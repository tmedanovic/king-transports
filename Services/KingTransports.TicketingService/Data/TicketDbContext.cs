using Microsoft.EntityFrameworkCore;
using MassTransit;
using Route = KingTransports.TicketingService.Entities.Route;
using KingTransports.TicketingService.Entities;

namespace KingTransports.TicketingService.Data
{
    public class TicketDbContext : DbContext
    {
        public TicketDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Station> Stations { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // add in memory outbox https://masstransit.io/documentation/patterns/in-memory-outbox
            builder.AddInboxStateEntity();
            builder.AddOutboxMessageEntity();
            builder.AddOutboxStateEntity();

            // builder.HasSequence<int>("PublicId_seq")
            //       .StartsAt(1000)
            //       .IncrementsBy(1);

            // builder.Entity<Ticket>()
            //             .Property(o => o.PublicId)
            //             .HasDefaultValueSql("nextval('\"PublicId_seq\"')");

        }

    }
}