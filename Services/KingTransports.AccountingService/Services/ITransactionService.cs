using KingTransports.AccountingService.DTOs;

namespace KingTransports.TicketingService.Services
{
    public interface ITransactionService
    {
        Task<TransactionDTO> CreateTransaction(CreateTransactionDTO createTransactionDTO);
        Task<List<TransactionDTO>> GetAllTransactions();
        Task<TransactionDTO> GetTransactionById(Guid id);
        decimal GetSaldo();
    }
}