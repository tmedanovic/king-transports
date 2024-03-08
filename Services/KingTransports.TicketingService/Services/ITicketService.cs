using KingTransports.Common.Collections;
using KingTransports.TicketingService.DTOs;

namespace KingTransports.TicketingService.Services
{
    public interface ITicketService
    {
        Task<TicketDto> CreateTicketAsync(CreateTicketDto createTicketDto);
        Task<PagedList<TicketDto>> GetAllTicketsAsync(int page = 1);
        Task<TicketDto> GetTicketByIdAsync(Guid id);
        Task RefundTicketAsync(Guid id);
    }
}