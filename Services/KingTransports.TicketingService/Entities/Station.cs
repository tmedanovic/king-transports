using System.ComponentModel.DataAnnotations;

namespace KingTransports.TicketingService.Entities;

public class Station
{
    [Key]
    public Guid StationId { get; set; }

    public string StationName { get; set; }

    public StationType StationType { get; set; }
}