using Microsoft.EntityFrameworkCore;
using KingTransports.AccountingService.Entities;

namespace KingTransports.TicketingService.Data
{
    public class AccountingDbContext : DbContext
    {
        public AccountingDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
    }
}