using KingTransports.TicketingService.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace KingTransports.TicketingService.Repositories
{
    public interface ITicketRepository
    {
        Task<Ticket> CreateTicket(Ticket ticket);
        Task<Ticket> GetTicketWithCildrenById(Guid id);
        public IIncludableQueryable<Ticket, Station> GetAllTicketsWithCildren();
        Task SetRefunded(Ticket ticket, bool refunded);
        Task<Ticket> GetTicketById(Guid id);
    }
}