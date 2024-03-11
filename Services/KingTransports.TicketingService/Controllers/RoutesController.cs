using KingTransports.Common.Collections;
using KingTransports.Common.Security;
using KingTransports.TicketingService.DTOs;
using KingTransports.TicketingService.Services;
using Microsoft.AspNetCore.Mvc;
using KingTransports.Common.Extensions;

namespace KingTransports.TicketingService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoutesController : ControllerBase
    {
        private readonly IRouteService _routeService;

        public RoutesController(IRouteService routeService)
        {
            _routeService = routeService;
        }

        [HttpGet]
        [ScopesAndAdminOrAnyOfRolesAuthorize(Roles = "ticket-seller")]
        public async Task<ActionResult<PagedList<RouteDto>>> GetAllRoutesPagedAsync([FromQuery] int page = 1)
        {
            var pagedList = await _routeService.GetAllRoutesPagedAsync(page);
            return this.OkPaged(pagedList);
        }

        [HttpGet("search")]
        [ScopesAndAdminOrAnyOfRolesAuthorize(Roles = "ticket-seller")]
        public async Task<ActionResult<PagedList<RouteDto>>> GetAllRoutesAsync()
        {
            var routes = await _routeService.GetAllRoutesAsync();
            return Ok(routes);
        }
    }
}