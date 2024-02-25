using KingTransports.TicketingService.DTOs;

namespace KingTransports.TicketingService.Services
{
    public interface ITicketService
    {
        Task<TicketDto> CreateTicket(CreateTicketDto createTicketDto);
        Task<List<TicketDto>> GetAllTickets();
        Task<TicketDto> GetTicketById(Guid id);
        Task RefundTicket(Guid id);
    }
}