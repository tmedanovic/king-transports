using KingTransports.Common.Security;
using KingTransports.TicketingService.DTOs;
using KingTransports.TicketingService.Services;
using KingTransports.Common.Extensions;
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
        [ScopesAndAdminOrAnyOfRolesAuthorize(Scopes = "ticket.validate", Roles = "conductor, ticket-seller")]
        public async Task<ActionResult<List<TicketDto>>> GetAllTicketsAsync([FromQuery] int page = 1)
        {
            var pagedList = await _ticketService.GetAllTicketsAsync(page);
            return this.OkPaged(pagedList);
        }

        [HttpGet("{id}")]
        [ScopesAndAdminOrAnyOfRolesAuthorize(Scopes = "ticket.validate", Roles = "conductor, ticket-seller")]
        public async Task<ActionResult<TicketDto>> GetTicketByIdAsync(Guid id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            return ticket;
        }

        [HttpPost]
        [ScopesAndAdminOrAnyOfRolesAuthorize(Scopes = "ticket.validate", Roles = "ticket-seller")]
        public async Task<ActionResult<TicketDto>> CreateTicketAsync(CreateTicketDto createTicketDto)
        {
            var ticket = await _ticketService.CreateTicketAsync(createTicketDto);
            return CreatedAtAction(nameof(GetTicketByIdAsync), new { id = ticket.TicketId }, ticket);
        }

        [HttpPost("{id}/refund")]
        public async Task<ActionResult> RefundTicketAsync([FromRoute] Guid id)
        {
            await _ticketService.RefundTicketAsync(id);
            return Ok();
        }
    }
}