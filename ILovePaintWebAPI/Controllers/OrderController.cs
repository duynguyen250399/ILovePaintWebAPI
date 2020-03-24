using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using ILovePaintWebAPI.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.OrderService;

namespace ILovePaintWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IWebHostEnvironment _env;

        public OrderController(IOrderService orderService, IWebHostEnvironment env)
        {
            _orderService = orderService;
            _env = env;

        }

        [HttpPost]
        public IActionResult PostOrder([FromBody] OrderModel orderData)
        {
            if (orderData == null || orderData.OrderItems == null || orderData.OrderItems.Count == 0
                || orderData.Order == null)
            {
                return BadRequest("Invalid order information");
            }
            //return Ok(orderData.OrderItems);

            var newOrder = _orderService.AddOrder(orderData);

            EmailHandler emailHandler = new EmailHandler(_env);
            List<OrderItemEmail> orderItemEmails = new List<OrderItemEmail>();
            foreach (var item in orderData.OrderItems)
            {
                var i = new OrderItemEmail
                {
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    Amount = item.Amount,
                    UnitPrice = item.UnitPrice,
                    VolumeValue = item.VolumeValue
                };
                orderItemEmails.Add(i);
            }

            OrderEmailModel orderEmail = new OrderEmailModel
            {
                Order = newOrder,
                OrderItems = orderItemEmails
            };

            emailHandler.SendOderConfirmEmail(orderEmail);

            return Ok(new
            {
                message = "Order has been sent",
                confirm_email = "sent",
                order = newOrder
            });
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            var orders = _orderService.GetOrders();
            if (orders == null)
            {
                return NotFound("Order list is empty");
            }

            return Ok(orders);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetOrder(long id)
        {
            var order = _orderService.GetOrderByID(id);
            if (order == null)
            {
                return NotFound("Order not found!");
            }

            foreach (var o in order.OrderItems)
            {
                o.Product.Image = Utils.ImagePathToLink(o.Product.ID);
            }

            return Ok(order);
        }

        [HttpPut]
        public IActionResult UpdateOrder(UpdateOrderModel model)
        {
            var newOrder = _orderService.UpdateOrder(model);
            if (newOrder == null)
            {
                return NotFound(new { message = "Order not found!" });
            }

            return Ok(newOrder);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteOrder(long id)
        {
            var order = _orderService.DeleteOrder(id);
            if (order == null)
            {
                return NotFound(new { message = "Order not found!" });
            }

            return Ok(order);
        }
    }
}