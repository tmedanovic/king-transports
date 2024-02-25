using KingTransports.FleetService.Data;
using KingTransports.FleetService.Entities;

namespace KingTransports.FleetService.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly FleetDbContext _context;

        public VehicleRepository(FleetDbContext context)
        {
            _context = context;
        }

        public async Task<Vehicle> GetVehicleById(Guid id)
        {
            return await _context.Vehicles.FindAsync(id);
        }

        public async Task<Vehicle> CreateVehicle(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            return vehicle;
        }
    }
}
