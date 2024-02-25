using KingTransports.TicketingService.Data;
using KingTransports.TicketingService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Route = KingTransports.TicketingService.Entities.Route;

namespace KingTransports.TicketingService.Repositories
{
    public class RouteRepository : IRouteRepository
    {
        private readonly TicketDbContext _context;

        public RouteRepository(TicketDbContext context)
        {
            _context = context;
        }

        public async Task<Route> GetRouteById(Guid id)
        {
            return await _context.Routes.FindAsync(id);
        }

        public async Task<Route> GetRouteWithCildrenById(Guid id)
        {
            return await GetAllRoutesWithCildren().SingleOrDefaultAsync(x => x.RouteId == id);
        }

        public async Task<Route> CreateRoute(Route route)
        {
            _context.Routes.Add(route);
            await _context.SaveChangesAsync();

            return route;
        }

        public IIncludableQueryable<Route, Station> GetAllRoutesWithCildren()
        {
            var query = _context.Routes.Include(x => x.StationFrom)
                .Include(x => x.StationTo);

            return query;
        }
    }
}
