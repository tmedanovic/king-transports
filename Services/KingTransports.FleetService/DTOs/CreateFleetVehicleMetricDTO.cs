using System.ComponentModel.DataAnnotations;

namespace KingTransports.FleetService.DTOs
{
    public class CreateFleetVehicleMetricDTO
    {
        [Required]
        public Guid FleetVehicleId { get; set; }
        [Required]
        public decimal OdometerKm { get; set; }
        [Required]
        public DateTime LoggedAt { get; set; }
    }
}
