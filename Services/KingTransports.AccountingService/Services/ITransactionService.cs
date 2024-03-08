using KingTransports.AccountingService.DTOs;

namespace KingTransports.TicketingService.Services
{
    public interface ITransactionService
    {
        Task<TransactionDTO> CreateTransactionAsync(CreateTransactionDTO createTransactionDTO);
        Task<List<TransactionDTO>> GetAllTransactionsAsync();
        Task<TransactionDTO> GetTransactionByIdAsync(Guid id);
        decimal GetSaldo();
    }
}