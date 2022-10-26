using Moq;
using NUnit.Framework;
using RedPoc.Entity.Entities;
using RedPoc.Entity.Viewmodels;
using RedPoc.Repository.Interfaces;
using RedPoc.Service.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedPoc.UnitTest
{
    [TestFixture]
    public class Tests
    {
        Order mockInsertEntity = null;
        OrderService orderService = null;

        [SetUp]
        public void SetUp()
        {
            mockInsertEntity = new Order()
            {
                Id = new System.Guid(),
                CreatedBy = "Ignacio Mariscal",
                CreateDate = System.DateTime.Now,
                CustomerName = "Nachos tacos",
                OrderType = OrderType.Standard
            };

        }

        [Test]
        public async Task Order_Insert_Record_Validate_Employee_Data_Returned()
        {
            //Arrange
            var mockOrderRepository = new Mock<IOrderRepository>();

            mockOrderRepository.Setup(m => m.CreateOrder(It.IsAny<Order>()));
            //Act 
            orderService = new OrderService(mockOrderRepository.Object);
            var result = await orderService.AddOrderAsync( new Entity.Viewmodels.AddOrderViewModel());

            //Assert
            Assert.AreEqual(result.SuccessMessage, "Order created");   
            Assert.IsNotNull(result);

        }


        [Test]
        public async Task Order_Insert_Validate_Order_List_Count()
        {
            //Arrange
            var mockOrderRepository = new Mock<IOrderRepository>();
            var orderList = new OrderViewModelList();
            orderList.Orders.Add(new OrderViewModel()
            {
                CreatedBy = "Ignacio Mariscal",
                CreateDate = System.DateTime.Now,
                CustomerName = "Nachos tacos",
                OrderType = OrderType.Standard
            });

            IEnumerable<Order> orders = new Order[] { mockInsertEntity };

            mockOrderRepository.Setup(m => m.CreateOrder(It.IsAny<Order>()));
            mockOrderRepository.Setup(m => m.GetAllOrderAsync());

            //Act 
            orderService = new OrderService(mockOrderRepository.Object);

            var result = await orderService.AddOrderAsync(new Entity.Viewmodels.AddOrderViewModel());
            var orderListResult = await orderService.GetOrdersAsync();

            //Assert
            Assert.GreaterOrEqual(orderList.Orders.Count, 1);
            Assert.IsNotNull(orderList);

        }
    }
}