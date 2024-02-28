using AutoMapper;
using KingTransports.TicketingService.DTOs;
using KingTransports.TicketingService.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using KingTransports.Common.Errors;
using KingTransports.TicketingService.Entities;
using KingTransports.Common.Events;

namespace KingTransports.TicketingService.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IRouteRepository _routeRepository;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public TicketService(ITicketRepository ticketRepository,
            IRouteRepository routeRepository,
            IMapper mapper,
            IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
            _mapper = mapper;
            _ticketRepository = ticketRepository;
            _routeRepository = routeRepository;
        }

        public async Task<List<TicketDto>> GetAllTickets()
        {
            var tickets = await _ticketRepository.GetAllTicketsWithCildren()
            .OrderByDescending(x => x.IssuedAt)
            .ToListAsync();

            return _mapper.Map<List<TicketDto>>(tickets);
        }

        public async Task<TicketDto> GetTicketById(Guid id)
        {
            var foundTicket = await _ticketRepository.GetTicketWithCildrenById(id);

            if (foundTicket == null)
            {
                throw new NotFound("ticket_not_found");
            }

            return _mapper.Map<TicketDto>(foundTicket);
        }

        public async Task<TicketDto> CreateTicket(CreateTicketDto createTicketDto)
        {
            var route = await _routeRepository.GetRouteById(createTicketDto.RouteId);

            if (route == null)
            {
                throw new NotFound("route_not_found");
            }

            var utcNow = DateTime.UtcNow;

            var ticket = new Ticket();
            ticket.TicketId = Guid.NewGuid();
            ticket.RouteId = createTicketDto.RouteId;
            ticket.IssuedAt = utcNow;
            ticket.ValidFrom = utcNow;
            ticket.ValidTo = utcNow.AddDays(1);
            ticket.Price = (decimal)(route.DistanceKm * 0.5);

            var ticketCreated = _mapper.Map<TicketCreated>(ticket);
            await _publishEndpoint.Publish(ticketCreated);
            await _ticketRepository.CreateTicket(ticket);

            var newTicket = _mapper.Map<TicketDto>(ticket);
            return newTicket;
        }

        public async Task RefundTicket(Guid id)
        {
            var ticket = await _ticketRepository.GetTicketById(id);

            if (ticket == null)
            {
                throw new NotFound("ticket_not_found");
            }

            if (ticket.Refunded)
            {
                throw new InvalidRequest("ticket_already_refunded");
            }

            var ticketRefunded = new TicketRefunded()
            {
                TicketId = id,
                Price = ticket.Price
            };

            await _publishEndpoint.Publish(ticketRefunded);
            await _ticketRepository.SetRefunded(ticket, true);
        }
    }
}
