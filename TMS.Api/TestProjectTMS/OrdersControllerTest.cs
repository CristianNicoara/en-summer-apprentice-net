using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections;
using TMS.Api.Controllers;
using TMS.Api.Models;
using TMS.Api.Models.DTOs;
using TMS.Api.Repositories;

namespace TestProjectTMS
{
    [TestClass]
    public class OrdersControllerTest
    {
        Mock<IOrderRepository> _orderRepositoryMock;
        Mock<ITicketCategoryRepository> _ticketCategoryRepositoryMock;
        Mock<IMapper> _mapperMoq;
        List<Order> _moqList;
        List<OrderDTO> _dtoMoq;

        [TestInitialize]
        public void SetupMoqData()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _ticketCategoryRepositoryMock = new Mock<ITicketCategoryRepository>();
            _mapperMoq = new Mock<IMapper>();
            _moqList = new List<Order>
            {
                new Order {OrderId = 1,
                    CustomerId = 4,
                    TicketCategoryId = 2,
                    OrderedAt = DateTime.Now,
                    NumberOfTickets = 2,
                    TotalPrice = 1400.0,
                    Customer = new Customer {CustomerId = 4, CustomerEmail = "Mock Email", CustomerName = "Mock Name"},
                    TicketCategory = new TicketCategory {TicketCategoryId = 2, EventId = 1, TicketCategoryDescription = "mock description", TicketCategoryPrice = 500.0}
                }
            };
            _dtoMoq = new List<OrderDTO>
            {
                new OrderDTO
                {
                    OrderId = 1,
                    TicketCategoryId = 2,
                    OrderedAt = DateTime.Now,
                    NumberOfTickets = 2,
                    TotalPrice = 1400.0
                }
            };
        }

        [TestMethod]
        public async Task GetAllOrdersReturnListOfOrders()
        {
            //Arrange

            IEnumerable<Order> moqOrders = _moqList;
            Task<IEnumerable<Order>> moqTask = Task.Run(() => moqOrders);
            _orderRepositoryMock.Setup(moq => moq.GetOrders()).Returns(moqTask);

            _mapperMoq.Setup(moq => moq.Map<IEnumerable<OrderDTO>>(It.IsAny<IReadOnlyList<Order>>())).Returns(_dtoMoq);

            var controller = new OrderController(_orderRepositoryMock.Object, _mapperMoq.Object, _ticketCategoryRepositoryMock.Object);

            //Act
            var orders = await controller.GetOrders();
            var orderResult = orders.Result as OkObjectResult;
            var orderCount = orderResult.Value as List<OrderDTO>;

            //Assert

            Assert.AreEqual(_moqList.Count, orderCount.Count);
        }

        [TestMethod]
        public async Task GetOrderByIdReturnNotFoundWhenNoRecordFound()
        {
            //Arrange
            _orderRepositoryMock.Setup(moq => moq.GetById(It.IsAny<int>())).Returns(Task.Run(() => _moqList.First()));
            _mapperMoq.Setup(moq => moq.Map<IEnumerable<OrderDTO>>(It.IsAny<IReadOnlyList<Event>>())).Returns((IEnumerable<OrderDTO>)null);
            var controller = new OrderController(_orderRepositoryMock.Object, _mapperMoq.Object, _ticketCategoryRepositoryMock.Object);
            //Act

            var result = await controller.GetOrderById(1);
            var orderResult = result.Result as NotFoundResult;


            //Assert

            Assert.IsTrue(orderResult.StatusCode == 404);
        }

        [TestMethod]
        public async Task GetOrderByIdReturnFirstRecord()
        {
            //Arrange
            _orderRepositoryMock.Setup(moq => moq.GetById(It.IsAny<int>())).Returns(Task.Run(() => _moqList.First()));
            _mapperMoq.Setup(moq => moq.Map<OrderDTO>(It.IsAny<Order>())).Returns(_dtoMoq.First());
            var controller = new OrderController(_orderRepositoryMock.Object, _mapperMoq.Object, _ticketCategoryRepositoryMock.Object);
            //Act

            var result = await controller.GetOrderById(1);
            var orderResult = result.Result as OkObjectResult;
            var orderCount = orderResult.Value as OrderDTO;

            //Assert

            Assert.AreEqual(1, orderCount.OrderId);
        }


        [TestMethod]
        public async Task GetOrderByIDThrowsAnException()
        {
            //Arrange
            _orderRepositoryMock.Setup(moq => moq.GetById(It.IsAny<int>())).Throws<Exception>();
            _mapperMoq.Setup(moq => moq.Map<OrderDTO>(It.IsAny<Order>())).Returns(_dtoMoq.First());
            var controller = new OrderController(_orderRepositoryMock.Object, _mapperMoq.Object, _ticketCategoryRepositoryMock.Object);
            //Act

            var result = await controller.GetOrderById(1);

            //Assert

            Assert.IsNull(result);
        }
    }
}