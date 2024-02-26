namespace KingTransports.Common.Events;

public class TicketRefunded
{
    public Guid TicketId { get; set; }

    public decimal Price { get; set; }
}
