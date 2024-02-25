using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KingTransports.FleetService.Entities;

public class FleetVehicle
{
    [Key]
    public Guid FleetVehicleId { get; set; }

    public string VehicleVin { get; set; }

    [ForeignKey("Vehicle")]
    public Guid VehicleId { get; set; }

    public virtual Vehicle Vehicle { get; set; }
    
    public DateTime InServiceFrom { get; set; }
    
    public DateTime? InServiceTo { get; set; }
    
    public FleetVehicleOperabilityStatus VehicleOperabilityStatus { get; set; }
}