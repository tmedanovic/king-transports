using AutoMapper;
using KingTransports.Common.Events;
using KingTransports.TicketingService.DTOs;
using KingTransports.TicketingService.Entities;
using Route = KingTransports.TicketingService.Entities.Route;

namespace KingTransports.TicketingService.Helpers
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<Ticket, TicketDto>().IncludeMembers(x => x.Route);
            CreateMap<Route, RouteDto>().IncludeMembers(x => x.StationFrom).IncludeMembers(x => x.StationTo);
            CreateMap<Station, StationDto>();
            CreateMap<Route, TicketDto>();
            CreateMap<Station, RouteDto>();
            CreateMap<Ticket, TicketCreated>();
        }
    }
}