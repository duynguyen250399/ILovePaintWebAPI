using DataLayer.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ILovePaintWebAPI.Helpers
{
    public class EmailHandler
    {
        private readonly IWebHostEnvironment _env;

        public EmailHandler(IWebHostEnvironment env)
        {
            _env = env;
        }

        public EmailHandler()
        {

        }

        public bool SendOderConfirmEmail(OrderEmailModel order)
        {

            if (order == null)
            {
                return false;
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

            return true;

        }

        public bool SendAccountConfirmEmail(string email, string displayName, string subject, string confirmationLink) 
        {
            if(string.IsNullOrEmpty(displayName) || string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(confirmationLink))
            {
                return false;
            }

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("ilovepaint371@gmail.com", "bangbangzz371");

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("ilovepaint371@gmail.com", displayName);
            mail.To.Add(email);
            mail.Subject = subject;

            mail.IsBodyHtml = true;

            EmailTemplateProcessor templateProcessor = new EmailTemplateProcessor(_env);
            var accountConfirmEmail = templateProcessor.GenerateAccountConfirmEmail(confirmationLink);

            mail.Body = accountConfirmEmail;

            client.Send(mail);

            return true;
        }
    }
}
