namespace KingTransports.Common.Events
{
    public class TransactionCreated
    {
        public Guid Transactionid { get; set; }

        public decimal Amount { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
