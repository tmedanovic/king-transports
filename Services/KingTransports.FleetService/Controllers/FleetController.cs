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
        public async Task<ActionResult<List<FleetVehicleDTO>>> GetAllFleetVehicles()
        {
            var fleetVehicles = await _fleetVehicleService.GetAllFleetVehicles();
            return fleetVehicles;
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "fleet.read")]
        public async Task<ActionResult<FleetVehicleDTO>> GetFleetVehicleById(Guid id)
        {
            var fleetVehicle = await _fleetVehicleService.GetFleetVehicleById(id);
            return fleetVehicle;
        }

        [HttpPost]
        [Authorize(Policy = "fleet.create")]
        public async Task<ActionResult<FleetVehicleDTO>> CreateFleetVehicle(CreateFleetVehicleDTO createFleetVehicleDTO)
        {
            var fleetVehicle = await _fleetVehicleService.CreateFleetVehicle(createFleetVehicleDTO);
            return CreatedAtAction(nameof(GetFleetVehicleById), new { id = fleetVehicle.FleetVehicleId }, fleetVehicle);
        }
    }
}