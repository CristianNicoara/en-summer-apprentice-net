namespace TMS.Api.Models.DTOs
{
    public class VenueDTO
    {
        public int VenueId { get; set; }

        public string? VenueLocation { get; set; }

        public string? VenueType { get; set; }

        public int? VenueCapacity { get; set; }
    }
}
