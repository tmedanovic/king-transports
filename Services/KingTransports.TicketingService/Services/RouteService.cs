using AutoMapper;
using KingTransports.Common.Collections;
using KingTransports.TicketingService.DTOs;
using KingTransports.TicketingService.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KingTransports.TicketingService.Services
{
    public class RouteService : IRouteService
    {
        private readonly IRouteRepository _routeRepository;
        private readonly IMapper _mapper;

        public RouteService(
            IRouteRepository routeRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _routeRepository = routeRepository;
        }

        public async Task<List<RouteDto>> GetAllRoutesAsync()
        {
            var query = _routeRepository.GetAllRoutesWithCildren()
                .OrderByDescending(x => x.RouteId);

            var mapped = _mapper.ProjectTo<RouteDto>(query);
            var routes = await mapped.ToListAsync();

            return routes;
        }

        public async Task<PagedList<RouteDto>> GetAllRoutesPagedAsync(int page = 1)
        {
            var query = _routeRepository.GetAllRoutesWithCildren()
                .OrderByDescending(x => x.RouteId);

            var mapped = _mapper.ProjectTo<RouteDto>(query);
            var pagedList = await PagedList<RouteDto>.ToPagedListAsync(mapped, page);

            return pagedList;
        }
    }
}
