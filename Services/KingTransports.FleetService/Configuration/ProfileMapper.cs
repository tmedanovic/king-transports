using AutoMapper;
using KingTransports.Common.Events;
using KingTransports.FleetService.DTOs;
using KingTransports.FleetService.Entities;

namespace KingTransports.FleetService.Configuration
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<Vehicle, VehicleDTO>();
            CreateMap<FleetVehicle, FleetVehicleDTO>().IncludeMembers(x => x.Vehicle);
            CreateMap<FleetVehicleDTO, FleetVehicleCreated>().IncludeMembers(x => x.Vehicle);
            CreateMap<Vehicle, FleetVehicleDTO>();
            CreateMap<Vehicle, FleetVehicleCreated>();
        }
    }
}