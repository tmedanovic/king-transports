using System.ComponentModel.DataAnnotations;

namespace KingTransports.AccountingService.Entities
{
    public class Transaction
    {
        [Key]
        public Guid Transactionid { get; set; }

        public decimal Amount { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
