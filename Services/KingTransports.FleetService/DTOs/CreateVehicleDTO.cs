using KingTransports.FleetService.Entities;
using System.ComponentModel.DataAnnotations;

namespace KingTransports.FleetService.DTOs
{
    public class CreateVehicleDTO
    {
        [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public int MaintanceKm { get; set; }
        [Required]
        public int MaintanceMonths { get; set; }
        [Required]
        public VehicleType VehicleType { get; set; }
    }
}
