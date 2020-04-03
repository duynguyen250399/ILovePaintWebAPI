using DataLayer.Entities;
using DataLayer.Models;
using System.Collections.Generic;

namespace ServiceLayer.OrderService
{
    public interface IOrderService
    {
        Order AddOrder(OrderModel orderData);
        Order DeleteOrder(long id);
        IEnumerable<Order> GetOrders();
        Order GetOrderByID(long id);
        Order UpdateOrder(UpdateOrderModel model);
    }
}
