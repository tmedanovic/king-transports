using KingTransports.TicketingService.Data;
using Microsoft.EntityFrameworkCore;

namespace KingTransports.AccountingService.Data
{
    public class DbInitializer
    {
        public static void InitDb(WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            SeedData(scope.ServiceProvider.GetService<AccountingDbContext>());
        }

        private static void SeedData(AccountingDbContext context)
        {
            context.Database.Migrate();
        }
    }
}
