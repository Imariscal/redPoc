using RedPoc.Entity.Entities;
using RedPoc.Entity.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedPoc.Service.Interfaces
{
    public interface IOrderService
    {
        Task<OrderViewModelList> GetOrdersAsync();
        Task<OrderViewModel> GetOrderByIdAsync(Guid orderId);
        Task<OrderViewModel> AddOrderAsync(AddOrderViewModel order);
        Task<OrderViewModel> DeleteOrderAsync(Guid orderId);
        Task<OrderViewModel> UpdateOrderAsync(UpdateOrderViewModel order);
        Task<OrderViewModelList> GetOrdersByOrderTypeAsync(OrderType orderType);
    }
}
