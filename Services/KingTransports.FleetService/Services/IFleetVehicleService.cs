using KingTransports.FleetService.DTOs;

namespace KingTransports.FleetService.Services
{
    public interface IFleetVehicleService
    {
        Task<FleetVehicleDTO> CreateFleetVehicle(CreateFleetVehicleDTO createFleetVehicleDTO);
        Task<List<FleetVehicleDTO>> GetAllFleetVehicles();
        Task<FleetVehicleDTO> GetFleetVehicleById(Guid id);
    }
}