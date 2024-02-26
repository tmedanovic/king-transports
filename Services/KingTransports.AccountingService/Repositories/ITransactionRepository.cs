using KingTransports.AccountingService.Entities;

namespace KingTransports.TicketingService.Repositories
{
    public interface ITransactionRepository
    {
        Task<Transaction> CreateTransaction(Transaction transaction);
        IQueryable<Transaction> GetAllTransactions();
        Task<Transaction> GetTransactionById(Guid id);
        decimal GetSaldo();
    }
}