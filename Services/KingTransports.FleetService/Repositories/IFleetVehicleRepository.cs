using KingTransports.FleetService.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace KingTransports.FleetService.Repositories
{
    public interface IFleetVehicleRepository
    {
        Task<FleetVehicle> CreateFleetVehicle(FleetVehicle fleetVehicle);
        Task<FleetVehicle> GetFleetVehicleWithCildrenById(Guid id);
        public IIncludableQueryable<FleetVehicle, Vehicle> GetAllFleetVehicleWithCildren();
        Task<FleetVehicle> GetFleetVehicleById(Guid id);
    }
}