using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TMS.Api.Models;
using TMS.Api.Models.DTOs;
using TMS.Api.Repositories;

namespace TMS.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ITicketCategoryRepository _ticketCategoryRepository;

        public OrderController(IOrderRepository orderRepository, IMapper mapper, ITicketCategoryRepository ticketCategoryRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _ticketCategoryRepository = ticketCategoryRepository;
        }

        [HttpGet]
        public ActionResult<List<OrderDTO>> GetOrders()
        {
            var orders = _orderRepository.GetOrders();

            var dtoOrders = orders.Select(o => new OrderDTO()
            {
                OrderId = o.OrderId,
                OrderedAt = o.OrderedAt,
                TicketCategoryId = o.TicketCategoryId,
                NumberOfTickets = o.NumberOfTickets,
                TotalPrice = o.TotalPrice
            });

            return Ok(dtoOrders);
        }

        [HttpGet]
        public async Task<ActionResult<EventDTO>> GetOrderById(int id)
        {
            var order = await _orderRepository.GetById(id);

            if (order == null)
                return NotFound();

            var dtoOrder = _mapper.Map<OrderDTO>(order);

            return Ok(dtoOrder);
        }

        [HttpPatch]
        public async Task<ActionResult<OrderPatchDTO>> PatchOrder(OrderPatchDTO orderPatch)
        {
            var orderEntity = await _orderRepository.GetById(orderPatch.OrderId);

            var ticket = await _ticketCategoryRepository.GetById(orderPatch.TicketCategoryId ?? 0);

            if (orderEntity == null)
                return NotFound();

            if (orderPatch.TicketCategoryId.HasValue)
                orderEntity.TicketCategoryId = orderPatch.TicketCategoryId;

            if (orderPatch.NumberOfTickets.HasValue)
                orderEntity.NumberOfTickets = orderPatch.NumberOfTickets;

            orderEntity.TotalPrice = (orderPatch.NumberOfTickets ?? 0) * (ticket.TicketCategoryPrice);

            _orderRepository.Update(orderEntity);
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var orderEntity = await _orderRepository.GetById(id);

            if (orderEntity == null)
                return NotFound();

            _orderRepository.Delete(orderEntity);
            return NoContent();
        }
    }
}
