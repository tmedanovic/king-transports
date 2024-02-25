using KingTransports.FleetService.Entities;
using System.ComponentModel.DataAnnotations;

namespace KingTransports.FleetService.DTOs
{
    public class CreateFleetVehicleMaintanceLogDTO
    {
        [Required]
        public Guid FleetVehicleId { get; set; }
        [Required]
        public virtual FleetVehicle FleetVehicle { get; set; }
        [Required]
        public decimal OdometerKm { get; set; }
        [Required]
        public FleetVehicleMaintainceReason MaintainceType { get; set; }
        [Required]
        public string MaintanceDescription { get; set; }
        [Required]
        public DateTime MaintanceStartedAt { get; set; }
        [Required]
        public DateTime MaintanceFinishedAt { get; set; }
        [Required]
        public decimal MaintanceCost { get; set; }
        [Required]
        public FleetVehicleOperabilityStatus NewVehicleOperabilityStatus { get; set; }
    }
}
