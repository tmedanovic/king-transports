namespace KingTransports.Common.Events
{
    public class FleetVehicleCreated
    {
        public Guid FleetVehicleId { get; set; }

        public string VehicleVin { get; set; }

        public Guid VehicleId { get; set; }

        public DateTime InServiceFrom { get; set; }

        public DateTime? InServiceTo { get; set; }
    }
}
