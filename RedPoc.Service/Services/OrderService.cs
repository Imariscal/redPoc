using RedPoc.Entity.Entities;
using RedPoc.Entity.Viewmodels;
using RedPoc.Repository.Interfaces;
 
using RedPoc.Service.Interfaces;
 
namespace RedPoc.Service.Services
{
    public class OrderService : IOrderService
    {
        private const string DOES_NOT_EXIST = "Order Does not exist";
        private const string DELETE_MESSAGE = "Order deleted";
        private const string ENTITY_CREATED = "Order created";
        private const string ENTITY_UPDATED = "Order updated";

        private readonly IOrderRepository orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async Task<OrderViewModel> AddOrderAsync(AddOrderViewModel order)
        {
            var orderViewModel = new OrderViewModel();
            try
            {
                /// Todo : add Automapper instead to do ti manually
                var orderEntity = new Order
                {
                    CreateDate = DateTime.Now,
                    CreatedBy = order.CreatedBy,
                    CustomerName = order.CustomerName,
                    Id = new Guid(),
                    OrderType = order.OrderType
                };
                orderViewModel.SuccessMessage = ENTITY_CREATED;

                orderRepository.CreateOrder(orderEntity);
                await orderRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                orderViewModel.ErrorMessage = ex.Message;
            }

            return orderViewModel;
        }

        public async Task<OrderViewModel> DeleteOrderAsync(Guid orderId)
        {
            var orderViewModel = new OrderViewModel();
            try
            {
                var order = await orderRepository.GetOrderByIdAsync(orderId);

                if (order == null)
                {
                    orderViewModel.ErrorMessage = DOES_NOT_EXIST;
                }
                else
                {
                    orderRepository.DeleteOrder(order);
                    await orderRepository.SaveAsync();
                    orderViewModel.SuccessMessage = DELETE_MESSAGE;
                }
            }
            catch (Exception ex)
            {
                orderViewModel.ErrorMessage = ex.Message;
            }

            return orderViewModel;
        }

        public async Task<OrderViewModel> GetOrderByIdAsync(Guid orderId)
        {
            var orderViewModel = new OrderViewModel();
            try
            {
                var order = await orderRepository.GetOrderByIdAsync(orderId);

                if (order == null)
                {
                    orderViewModel.ErrorMessage = DOES_NOT_EXIST;
                }
                else
                {
                    orderViewModel = MapOrderToViewModel(order);
                }
            }
            catch (Exception ex)
            {
                orderViewModel.ErrorMessage = ex.Message;
            }

            return orderViewModel;
        }

        public async Task<OrderViewModelList> GetOrdersAsync()
        {
            var orderViewModelList = new OrderViewModelList();
            try
            {
                var orders = await orderRepository.GetAllOrderAsync();
                var employeesVM = orders.Select(_ => MapOrderToViewModel(_));
                orderViewModelList.Orders = employeesVM.ToList(); 
            }
            catch (Exception ex)
            {
                orderViewModelList.ErrorMessage = ex.Message;
            }
            return orderViewModelList;
        }

        public async Task<OrderViewModelList> GetOrdersByOrderTypeAsync(OrderType orderType)
        {
            var orderViewModelList = new OrderViewModelList();
            try
            {
                var orders = await orderRepository.GetOrderByTypeIdAsync(orderType);
                var employeesVM = orders.Select(_ => MapOrderToViewModel(_));
                orderViewModelList.Orders = employeesVM.ToList();
            }
            catch (Exception ex)
            {
                orderViewModelList.ErrorMessage = ex.Message;
            }
            return orderViewModelList;
        }

        public async Task<OrderViewModel> UpdateOrderAsync(UpdateOrderViewModel orderView)
        {
            var orderViewModel = new OrderViewModel();
            try
            {
                var order = await orderRepository.GetOrderByIdAsync(orderView.Id);

                if (order == null)
                {
                    orderViewModel.ErrorMessage = DOES_NOT_EXIST;
                }
                else
                {
                    var updatedOrder = MapViewModelToOrder(orderView);
                    orderRepository.DeleteOrder(updatedOrder);
                    await orderRepository.SaveAsync();
                    orderViewModel.SuccessMessage = ENTITY_UPDATED;
                }
            }
            catch (Exception ex)
            {
                orderViewModel.ErrorMessage = ex.Message;
            }

            return orderViewModel;
        }

        private OrderViewModel MapOrderToViewModel(Order entity)
        {
            return new OrderViewModel
            {
                Id = entity.Id,
                CustomerName = entity.CustomerName,
                CreatedBy = entity.CreatedBy,
                CreateDate = entity.CreateDate,
                OrderType = entity.OrderType
            };
        }

        private Order MapViewModelToOrder(UpdateOrderViewModel viewModel)
        {
            return new Order
            {
                Id = viewModel.Id,
                CustomerName = viewModel.CustomerName,
                CreatedBy = viewModel.CreatedBy,
                CreateDate = viewModel.CreateDate,
                OrderType = viewModel.OrderType
            };
        }
    }
}
