namespace TMS.Api.Models.DTOs
{
    public class EventPatchDTO
    {
        public int EventId { get; set; }
        public string? EventName { get; set; }
        public string? EventDescription { get; set; }
    }
}
