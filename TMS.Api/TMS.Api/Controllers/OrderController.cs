using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TMS.Api.Exceptions;
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
        public async Task<ActionResult<List<OrderDTO>>> GetOrders()
        {
            var orders = await _orderRepository.GetOrders();

            var dtoOrders = _mapper.Map<IEnumerable<OrderDTO>>(orders);

            return Ok(dtoOrders);
        }

        [HttpGet]
        public async Task<ActionResult<EventDTO>> GetOrderById(int id)
        {
            try
            {
                var order = await _orderRepository.GetById(id);

                var dtoOrder = _mapper.Map<OrderDTO>(order);

                if (dtoOrder == null)
                    return NotFound();

                return Ok(dtoOrder);
            }
            catch (EntityNotFoundException ex)
            {
                throw new EntityNotFoundException(ex.Message);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPatch]
        public async Task<ActionResult<OrderPatchDTO>> PatchOrder(OrderPatchDTO orderPatch)
        {
            if (orderPatch == null) throw new ArgumentNullException(nameof(orderPatch));

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
