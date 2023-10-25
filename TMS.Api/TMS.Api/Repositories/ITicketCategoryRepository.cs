using TMS.Api.Models;

namespace TMS.Api.Repositories
{

    public interface ITicketCategoryRepository
    {
        Task<TicketCategory> GetById(int id);
    }
}
