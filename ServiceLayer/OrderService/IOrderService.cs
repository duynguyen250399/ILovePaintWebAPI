using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.OrderService
{
    public interface IOrderService
    {
        Task<Order> AddOrderAsync(Order order);
        Task<Order> DeleteOrderAsync(long id);
        IEnumerable<Order> GetOrders();
        Order GetOrderByID(long id);
    }
}
