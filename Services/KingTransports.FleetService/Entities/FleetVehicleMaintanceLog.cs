using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KingTransports.FleetService.Entities;

public class FleetVehicleMaintanceLog
{
    [Key]
    public Guid FleetVehicleMaintanceLogId { get; set; }

    [ForeignKey("FleetVehicle")]
    public Guid FleetVehicleId { get; set; }

    public virtual FleetVehicle FleetVehicle { get; set; }

    public decimal OdometerKm { get; set; }
    
    public FleetVehicleMaintainceReason MaintainceType { get; set; }
    
    public string MaintanceDescription { get; set; }
    
    public DateTime MaintanceStartedAt { get; set; }
    
    public DateTime MaintanceFinishedAt { get; set; }
    
    public decimal MaintanceCost { get; set; }
    
    public FleetVehicleOperabilityStatus NewVehicleOperabilityStatus { get; set; }
}