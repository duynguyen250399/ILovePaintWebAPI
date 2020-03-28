using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DataLayer.Entities;
using DataLayer.Models;
using ILovePaintWebAPI.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.OrderService;
using ServiceLayer.UserService;

namespace ILovePaintWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _env;

        public OrderController(IOrderService orderService, IUserService userService, UserManager<User> userManager, IWebHostEnvironment env)
        {
            _orderService = orderService;
            _userService = userService;
            _userManager = userManager;
            _env = env;

        }

        [HttpPost]
        public async Task<IActionResult> PostOrder([FromBody] OrderModel orderData)
        {
            if (orderData == null || orderData.OrderItems == null || orderData.OrderItems.Count == 0
                || orderData.Order == null)
            {
                return BadRequest("Invalid order information");
            }

            float total = 0;


            var newOrder = _orderService.AddOrder(orderData);

            EmailHandler emailHandler = new EmailHandler(_env);
            List<OrderItemEmail> orderItemEmails = new List<OrderItemEmail>();
            foreach (var item in orderData.OrderItems)
            {
                total += item.Amount;
                var i = new OrderItemEmail
                {
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    Amount = item.Amount,
                    UnitPrice = item.UnitPrice,
                    VolumeValue = item.VolumeValue,
                    ColorName = item.ColorName,
                    ColorCode = item.ColorCode
                };
                orderItemEmails.Add(i);
            }

            OrderEmailModel orderEmail = new OrderEmailModel
            {
                Order = newOrder,
                OrderItems = orderItemEmails
            };

            emailHandler.SendOderConfirmEmail(orderEmail);

            // plus reward points for member after purchasing products
            var userId = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            var user = await _userManager.FindByIdAsync(userId);
            var rewardPointSuccess = "";
            if (user != null)
            {
                var updatedUser = _userService.AddRewardPoints(user, (int)Math.Floor(total / 10000));
                if(updatedUser != null)
                {
                    rewardPointSuccess = "Success";
                }
                else
                {
                    rewardPointSuccess = "Fail";
                }
             }

            return Ok(new
            {
                message = "Order has been sent",
                confirm_email = "sent",
                order = newOrder,
                addRewardPoints = rewardPointSuccess
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