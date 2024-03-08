using KingTransports.FleetService.DTOs;

namespace KingTransports.FleetService.Services
{
    public interface IFleetVehicleService
    {
        Task<FleetVehicleDTO> CreateFleetVehicleAsync(CreateFleetVehicleDTO createFleetVehicleDTO);
        Task<List<FleetVehicleDTO>> GetAllFleetVehiclesAsync();
        Task<FleetVehicleDTO> GetFleetVehicleByIdAsync(Guid id);
    }
}