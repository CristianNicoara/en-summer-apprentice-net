using Microsoft.EntityFrameworkCore;
using TMS.Api.Exceptions;
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

            if(ticketCategory == null)
            {
                throw new EntityNotFoundException(id, nameof(TicketCategory));
            }

            return ticketCategory;
        }
    }
}
