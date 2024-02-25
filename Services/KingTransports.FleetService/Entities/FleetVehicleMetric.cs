using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KingTransports.FleetService.Entities;

public class FleetVehicleMetric
{
    [Key]
    public Guid FleetVehicleMetricId { get; set; }

    [ForeignKey("FleetVehicle")]
    public Guid FleetVehicleId { get; set; }

    public virtual FleetVehicle FleetVehicle { get; set; }

    public decimal OdometerKm { get; set; }

    public DateTime LoggedAt { get; set; }
}