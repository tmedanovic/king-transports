using KingTransports.FleetService.Entities;

namespace KingTransports.FleetService.DTOs
{
    public class VehicleDTO
    {
        public Guid VehicleId { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public int MaintanceKm { get; set; }

        public int MaintanceMonths { get; set; }

        public VehicleType VehicleType { get; set; }
    }

}