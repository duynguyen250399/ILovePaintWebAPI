using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Data;
using DataLayer.Models;

namespace ServiceLayer.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Order> AddOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return order;
        }

        public Task<Order> DeleteOrderAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Order GetOrderByID(long id)
        {
            return _context.Orders.Where(order => order.ID == id)
                .FirstOrDefault();
        }

        public IEnumerable<Order> GetOrders()
        {
            return _context.Orders;
        }
    }
}
