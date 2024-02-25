namespace KingTransports.Common.Events;
public class TicketCreated
{
    public Guid TicketId { get; set; }

    public decimal Price { get; set; }

    public DateTime IssuedAt { get; set; }

    public DateTime ValidFrom { get; set; }

    public DateTime ValidTo { get; set; }

}
