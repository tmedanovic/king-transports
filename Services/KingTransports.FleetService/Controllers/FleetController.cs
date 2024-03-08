using KingTransports.FleetService.DTOs;
using KingTransports.FleetService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KingTransports.FleetService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FleetController : ControllerBase
    {
        private readonly IFleetVehicleService _fleetVehicleService;

        public FleetController(IFleetVehicleService fleetVehicleService)
        {
            _fleetVehicleService = fleetVehicleService;
        }

        [HttpGet]
        [Authorize(Policy = "fleet.read")]
        public async Task<ActionResult<List<FleetVehicleDTO>>> GetAllFleetVehiclesAsync()
        {
            var fleetVehicles = await _fleetVehicleService.GetAllFleetVehiclesAsync();
            return fleetVehicles;
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "fleet.read")]
        public async Task<ActionResult<FleetVehicleDTO>> GetFleetVehicleByIdAsync(Guid id)
        {
            var fleetVehicle = await _fleetVehicleService.GetFleetVehicleByIdAsync(id);
            return fleetVehicle;
        }

        [HttpPost]
        [Authorize(Policy = "fleet.create")]
        public async Task<ActionResult<FleetVehicleDTO>> CreateFleetVehicleAsync(CreateFleetVehicleDTO createFleetVehicleDTO)
        {
            var fleetVehicle = await _fleetVehicleService.CreateFleetVehicleAsync(createFleetVehicleDTO);
            return CreatedAtAction(nameof(GetFleetVehicleByIdAsync), new { id = fleetVehicle.FleetVehicleId }, fleetVehicle);
        }
    }
}