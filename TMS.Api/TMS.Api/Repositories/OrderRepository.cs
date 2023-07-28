using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TMS.Api.Exceptions;
using TMS.Api.Models;

namespace TMS.Api.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly TicketManagementSystemContext _dbContext;

        public OrderRepository()
        {
            _dbContext = new TicketManagementSystemContext();
        }

        public void Delete(Order order)
        {
            _dbContext.Remove(order);
            _dbContext.SaveChanges();
        }

        public async Task<Order> GetById(int id)
        {
            var order = await _dbContext.Orders.Where(e => e.OrderId == id).FirstOrDefaultAsync();

            if (order == null) {
                throw new EntityNotFoundException(id, nameof(Order));
            }

            return order;
        }

        public IEnumerable<Order> GetOrders()
        {
            var orders = _dbContext.Orders;

            return orders;
        }

        public void Update(Order order)
        {
            _dbContext.Entry(order).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
