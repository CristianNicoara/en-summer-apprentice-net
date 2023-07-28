using Microsoft.EntityFrameworkCore;
using TMS.Api.Models;

namespace TMS.Api.Repositories
{
    public class TicketCategoryRepository : ITicketCategoryRepository
    {
        private readonly TicketManagementSystemContext _dbContext;

        public TicketCategoryRepository()
        {
            _dbContext = new TicketManagementSystemContext();
        }
        public async Task<TicketCategory> GetById(int id)
        {
            var ticketCategory = await _dbContext.TicketCategories.Where(e => e.TicketCategoryId == id).FirstOrDefaultAsync();

            return ticketCategory;
        }
    }
}
