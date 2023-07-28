using TMS.Api.Models;

namespace TMS.Api.Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetOrders();

        Task<Order> GetById(int id);

        void Update(Order order);

        void Delete(Order order);
    }
}
