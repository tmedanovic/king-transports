using KingTransports.FleetService.Data;
using KingTransports.FleetService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace KingTransports.FleetService.Repositories
{
    public class FleetVehicleRepository : IFleetVehicleRepository
    {
        private readonly FleetDbContext _context;

        public FleetVehicleRepository(FleetDbContext context)
        {
            _context = context;
        }

        public async Task<FleetVehicle> GetFleetVehicleById(Guid id)
        {
            return await _context.FleetVehicles.FindAsync(id);
        }

        public async Task<FleetVehicle> GetFleetVehicleWithCildrenById(Guid id)
        {
            return await GetAllFleetVehicleWithCildren().SingleOrDefaultAsync(x => x.FleetVehicleId == id);
        }

        public async Task<FleetVehicle> CreateFleetVehicle(FleetVehicle fleetVehicle)
        {
            _context.FleetVehicles.Add(fleetVehicle);
            await _context.SaveChangesAsync();

            return fleetVehicle;
        }

        public IIncludableQueryable<FleetVehicle, Vehicle> GetAllFleetVehicleWithCildren()
        {
            var query = _context.FleetVehicles.Include(x => x.Vehicle);

            return query;
        }
    }
}
