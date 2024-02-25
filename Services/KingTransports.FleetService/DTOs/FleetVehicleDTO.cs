using KingTransports.FleetService.Entities;
namespace KingTransports.FleetService.DTOs
{
    public class FleetVehicleDTO
    {
        public Guid FleetVehicleId { get; set; }

        public string VehicleVin { get; set; }

        public VehicleDTO Vehicle { get; set; }

        public DateTime InServiceFrom { get; set; }

        public DateTime? InServiceTo { get; set; }

        public FleetVehicleOperabilityStatus VehicleOperabilityStatus { get; set; }
    }
}
