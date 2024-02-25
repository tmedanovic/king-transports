namespace KingTransports.TicketingService.DTOs
{
    public class RouteDto
    {
        public Guid RouteId { get; set; }

        public StationDto StationFrom { get; set; }

        public StationDto StationTo { get; set; }
    }
}
