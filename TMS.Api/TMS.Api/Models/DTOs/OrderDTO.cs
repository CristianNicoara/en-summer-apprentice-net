namespace TMS.Api.Models.DTOs
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public DateTime? OrderedAt { get; set; }
        public int? TicketCategoryId { get; set; }
        public int? NumberOfTickets { get; set; }

        public double? TotalPrice { get; set; }
    }
}
