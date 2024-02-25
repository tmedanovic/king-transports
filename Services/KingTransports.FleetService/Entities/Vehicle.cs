using System.ComponentModel.DataAnnotations;

namespace KingTransports.FleetService.Entities;

public class Vehicle
{
    [Key]
    public Guid VehicleId { get; set; }

    public string Make { get; set; }
    
    public string Model { get; set; }
    
    public int MaintanceKm { get; set; }
    
    public int MaintanceMonths { get; set; }
    
    public VehicleType VehicleType { get; set; }
}