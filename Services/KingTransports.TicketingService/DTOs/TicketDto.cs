namespace KingTransports.TicketingService.DTOs
{
    public class TicketDto
    {
        public Guid TicketId { get; set; }

        public RouteDto Route { get; set; }

        public decimal Price { get; set; }

        public DateTime IssuedAt { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }

        public bool Refunded { get; set; }
    }

}