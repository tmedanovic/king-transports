namespace KingTransports.AccountingService.DTOs
{
    public class TransactionDTO
    {
        public Guid Transactionid { get; set; }

        public decimal Amount { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
