using Microsoft.EntityFrameworkCore;
using RedPoc.Entity.Entities;
using RedPoc.Repository.Context;
using RedPoc.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedPoc.Repository.Repository
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository, IDisposable
    {

        private bool disposed = false;
        public OrderRepository(RedContext repositoryContext)
        : base(repositoryContext)
        {
        }             

        public async Task<IEnumerable<Order>> GetAllOrderAsync()
        {
            return await FindAll()
             .OrderBy(_ => _.CustomerName)
             .ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(Guid orderId)
        {
            return await FindByCondition(_ => _.Id == orderId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Order>> GetOrderByTypeIdAsync(OrderType orderType)
        {
            return await FindByCondition(_ => _.OrderType == orderType)
              .ToListAsync();
        }

        public void UpdateOrder(Order order)
        {
            Update(order);
        }
        public void CreateOrder(Order order)
        {
            Create(order);
        }
        public void DeleteOrder(Order order)
        {
            Delete(order);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                   this.RepositoryContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
