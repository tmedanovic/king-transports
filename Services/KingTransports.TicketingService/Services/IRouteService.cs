using KingTransports.Common.Collections;
using KingTransports.TicketingService.DTOs;

namespace KingTransports.TicketingService.Services
{
    public interface IRouteService
    {
        Task<List<RouteDto>> GetAllRoutesAsync();
        Task<PagedList<RouteDto>> GetAllRoutesPagedAsync(int page = 1);
    }
}