using KingTransports.TicketingService.Data;
using KingTransports.TicketingService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace KingTransports.TicketingService.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly TicketDbContext _context;

        public TicketRepository(TicketDbContext context)
        {
            _context = context;
        }

        public async Task<Ticket> GetTicketById(Guid id)
        {
            return await _context.Tickets.FindAsync(id);
        }

        public async Task<Ticket> GetTicketWithCildrenById(Guid id)
        {
            return await GetAllTicketsWithCildren().SingleOrDefaultAsync(x => x.TicketId == id);
        }

        public async Task<Ticket> CreateTicket(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return ticket;
        }

        public async Task SetRefunded(Ticket ticket, bool refunded)
        {
            ticket.Refunded = refunded;
            await _context.SaveChangesAsync();
        }

        public IIncludableQueryable<Ticket, Station> GetAllTicketsWithCildren()
        {
            var query = _context.Tickets.Include(x => x.Route)
                .ThenInclude(x => x.StationFrom)
                .Include(x => x.Route)
                .ThenInclude(x => x.StationTo);

            return query;
        }
    }
}
