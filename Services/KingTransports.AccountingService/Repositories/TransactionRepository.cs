using KingTransports.AccountingService.Entities;
using KingTransports.TicketingService.Data;

namespace KingTransports.TicketingService.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AccountingDbContext _context;

        public TransactionRepository(AccountingDbContext context)
        {
            _context = context;
        }

        public async Task<Transaction> GetTransactionById(Guid id)
        {
            return await _context.Transactions.FindAsync(id);
        }

        public IQueryable<Transaction> GetAllTransactions()
        {
            return _context.Transactions.AsQueryable();
        }

        public async Task<Transaction> CreateTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }

        public decimal GetSaldo()
        {
            return _context.Transactions.Sum(x => x.Amount);
        }
    }
}
