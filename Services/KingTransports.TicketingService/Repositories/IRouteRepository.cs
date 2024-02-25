using KingTransports.TicketingService.Entities;
using Microsoft.EntityFrameworkCore.Query;
using Route = KingTransports.TicketingService.Entities.Route;

namespace KingTransports.TicketingService.Repositories
{
    public interface IRouteRepository
    {
        Task<Route> CreateRoute(Route route);
        IIncludableQueryable<Route, Station> GetAllRoutesWithCildren();
        Task<Route> GetRouteWithCildrenById(Guid id);
        Task<Route> GetRouteById(Guid id);
    }
}