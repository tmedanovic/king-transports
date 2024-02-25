using AutoMapper;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using KingTransports.Common.Errors;
using KingTransports.Common.Events;
using KingTransports.FleetService.DTOs;
using KingTransports.FleetService.Repositories;
using KingTransports.FleetService.Entities;

namespace KingTransports.FleetService.Services
{
    public class FleetVehicleService : IFleetVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IFleetVehicleRepository _fleetVehicleRepository;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public FleetVehicleService(IVehicleRepository vehicleRepository,
        IFleetVehicleRepository fleetVehicleRepository,
            IMapper mapper,
            IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
            _mapper = mapper;
            _vehicleRepository = vehicleRepository;
            _fleetVehicleRepository = fleetVehicleRepository;
        }

        public async Task<List<FleetVehicleDTO>> GetAllFleetVehicles()
        {
            var fleetVehicles = await _fleetVehicleRepository.GetAllFleetVehicleWithCildren()
            .OrderByDescending(x => x.InServiceFrom)
            .ToListAsync();

            return _mapper.Map<List<FleetVehicleDTO>>(fleetVehicles);
        }

        public async Task<FleetVehicleDTO> GetFleetVehicleById(Guid id)
        {
            var fleetVehicle = await _fleetVehicleRepository.GetFleetVehicleWithCildrenById(id);

            if (fleetVehicle == null)
            {
                throw new NotFound("fleet_vehicle_not_found");
            }

            return _mapper.Map<FleetVehicleDTO>(fleetVehicle);
        }

        public async Task<FleetVehicleDTO> CreateFleetVehicle(CreateFleetVehicleDTO createFleetVehicleDTO)
        {
            var vehicle = await _vehicleRepository.GetVehicleById(createFleetVehicleDTO.VehicleId);

            if (vehicle == null)
            {
                throw new NotFound("vehicle_not_found");
            }

            var utcNow = DateTime.UtcNow;

            var fleetVehicle = new FleetVehicle()
            {
                FleetVehicleId = Guid.NewGuid(),
                VehicleId = createFleetVehicleDTO.VehicleId,
                VehicleVin = createFleetVehicleDTO.VehicleVin,
                InServiceFrom = DateTime.UtcNow,
                VehicleOperabilityStatus = FleetVehicleOperabilityStatus.Operable,
            };

            await _fleetVehicleRepository.CreateFleetVehicle(fleetVehicle);

            var newFleetVehicle = _mapper.Map<FleetVehicleDTO>(fleetVehicle);
            var fleetVehicleCreated = _mapper.Map<FleetVehicleCreated>(newFleetVehicle);

            await _publishEndpoint.Publish(fleetVehicleCreated);

            return newFleetVehicle;
        }
    }
}
