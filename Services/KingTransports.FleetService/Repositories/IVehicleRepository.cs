using KingTransports.FleetService.Entities;

namespace KingTransports.FleetService.Repositories
{
    public interface IVehicleRepository
    {
        Task<Vehicle> CreateVehicle(Vehicle vehicle);
        Task<Vehicle> GetVehicleById(Guid id);
    }
}