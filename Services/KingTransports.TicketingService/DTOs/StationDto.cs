using KingTransports.TicketingService.Entities;

namespace KingTransports.TicketingService.DTOs
{
    public class StationDto
    {
        public Guid StationId { get; set; }

        public string StationName { get; set; }

        public StationType StationType { get; set; }
    }
}
