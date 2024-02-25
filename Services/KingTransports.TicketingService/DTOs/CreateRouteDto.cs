using System.ComponentModel.DataAnnotations;

namespace KingTransports.TicketingService.DTOs
{
    public class CreateRouteDTO
    {
        [Required]
        public Guid StationFromId { get; set; }

        [Required]
        public Guid StationToId { get; set; }

        [Required]
        public int DistanceKm { get; set; }
    }
}
