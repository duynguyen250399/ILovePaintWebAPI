using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using DataLayer.Models;
using ILovePaintWebAPI.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ILovePaintWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public EmailController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost]
        [Route("order-confirm")]
        public IActionResult SendOderConfirmEmail(OrderEmailModel order)
        {

            if (order == null)
            {
                return BadRequest(new { message = "Invalid order information!" });
            }

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("ilovepaint371@gmail.com", "bangbangzz371");

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("ilovepaint371@gmail.com", "ILovePaint - Order Confirmation");
            mail.To.Add("khanhduyx2@gmail.com");
            mail.Subject = $"ILovePaint #{order.Order.ID} - We have received your order!";

            mail.IsBodyHtml = true;

            EmailTemplateProcessor mailProcessor = new EmailTemplateProcessor(_env);
            string contents = mailProcessor.GenerateOrderConfirmMail(order);

            mail.Body = contents;

            client.Send(mail);

            return Ok(new { message = "Order confirm email has been send!" });

        }
    }
}