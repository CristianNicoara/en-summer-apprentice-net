namespace TMS.Api.Models.DTOs
{
    public class OrderPatchDTO
    {
        public int OrderId { get; set; }
        public int? TicketCategoryId { get; set; }
        public int? NumberOfTickets { get; set; }
    }
}
