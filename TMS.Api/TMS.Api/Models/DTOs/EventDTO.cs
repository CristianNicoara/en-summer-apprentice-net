namespace TMS.Api.Models.DTOs
{
    public class EventDTO
    {
        public int EventId { get; set; }
        public VenueDTO? Venue { get; set; }
        public string Type { get; set; } = string.Empty;
        public string? EventDescription { get; set; }

        public string? EventName { get; set; }
        public DateTime? EventStartDate { get; set; }

        public DateTime? EventEndDate { get; set; }
    }
}
