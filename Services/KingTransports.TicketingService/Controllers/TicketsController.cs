using KingTransports.TicketingService.DTOs;
using KingTransports.TicketingService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KingTransports.TicketingService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet]
        [Authorize(Policy = "ticket.validate")]
        public async Task<ActionResult<List<TicketDto>>> GetAllTickets()
        {
            var tickets = await _ticketService.GetAllTickets();
            return tickets;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TicketDto>> GetTicketById(Guid id)
        {
            var ticket = await _ticketService.GetTicketById(id);
            return ticket;
        }

        [HttpPost]
        [Authorize(Policy = "ticket.issue")]
        public async Task<ActionResult<TicketDto>> CreateTicket(CreateTicketDto createTicketDto)
        {
            var ticket = await _ticketService.CreateTicket(createTicketDto);
            return CreatedAtAction(nameof(GetTicketById), new { id = ticket.TicketId }, ticket);
        }

        [HttpPost("{id}/refund")]
        public async Task<ActionResult> RefundTicket([FromRoute] Guid id)
        {
            await _ticketService.RefundTicket(id);
            return Ok();
        }
    }
}