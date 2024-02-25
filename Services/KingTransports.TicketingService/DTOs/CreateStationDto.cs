using KingTransports.TicketingService.Entities;
using System.ComponentModel.DataAnnotations;

namespace KingTransports.TicketingService.DTOs
{
    public class CreateStationDto
    {
        [Required]
        public string StationCode { get; set; }

        [Required]
        public string StationName { get; set; }

        [Required]
        public StationType StationType { get; set; }
    }
}
