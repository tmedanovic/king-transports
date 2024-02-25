using System.ComponentModel.DataAnnotations;

namespace KingTransports.FleetService.DTOs
{
    public class CreateFleetVehicleDTO
    {
        [Required]
        public string VehicleVin { get; set; }
        [Required]
        public Guid VehicleId { get; set; }
        [Required]
        public DateTime InServiceFrom { get; set; }
    }
}