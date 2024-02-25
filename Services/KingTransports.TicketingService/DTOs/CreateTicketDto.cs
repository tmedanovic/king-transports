using System.ComponentModel.DataAnnotations;

namespace KingTransports.TicketingService.DTOs
{
    public class CreateTicketDto
    {
        [Required]
        public Guid RouteId { get; set; }

        [Required]
        public bool TwoWay { get; set; }
    }
}