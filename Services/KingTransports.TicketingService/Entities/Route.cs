using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KingTransports.TicketingService.Entities;

public class Route
{
    private string _routeHumanReadableString = $"Trip from {0} to {1}";

    [Key]
    public Guid RouteId { get; set; }

    [ForeignKey("StationFrom"), Column(Order = 0)]
    public Guid StationFromId { get; set; }

    public virtual Station StationFrom { get; set; }

    [ForeignKey("StationTo"), Column(Order = 1)]
    public Guid StationToId { get; set; }

    public virtual Station StationTo { get; set; }

    public int DistanceKm { get; set; }

    [NotMapped]
    public string RouteName { get => string.Format(_routeHumanReadableString, StationFrom.StationName, StationTo); }

}