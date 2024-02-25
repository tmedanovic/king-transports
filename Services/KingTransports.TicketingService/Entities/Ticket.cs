using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KingTransports.TicketingService.Entities;

public class Ticket
{
    [Key]
    public Guid TicketId { get; set; }

    [ForeignKey("Route"), Column(Order = 0)]
    public Guid RouteId { get; set; }

    public virtual Route Route { get; set; }

    public decimal Price { get; set; }

    public DateTime IssuedAt { get; set; }

    public DateTime ValidFrom { get; set; }

    public DateTime ValidTo { get; set; }

    public bool Refunded { get; set; }
}