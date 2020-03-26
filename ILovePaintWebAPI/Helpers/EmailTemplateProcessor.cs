
using DataLayer.Entities;
using DataLayer.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ILovePaintWebAPI.Helpers
{
    public class EmailTemplateProcessor
    {
        private readonly IWebHostEnvironment _env;
        public EmailTemplateProcessor(IWebHostEnvironment env)
        {
            _env = env;
        }

        public string GenerateOrderConfirmMail(OrderEmailModel order)
        {
            var path = _env.WebRootPath + "\\Email_Templates\\order_confirm.html";
            string contents = "";
      
            
            using (StreamReader reader = File.OpenText(path))
            {
               contents = reader.ReadToEnd();
            }

            string orderItemTableBody = GenerateOrderItemTable(order.OrderItems);

            contents = string.Format(contents, order.Order.ID,
                String.Format("{0:dddd, d MMMM yyyy}", order.Order.OrderDate),
                order.Order.FullName,
                order.Order.PhoneNumber,
                order.Order.Email,
                order.Order.Address,
                orderItemTableBody);

            return contents;
        }

        private string GenerateOrderItemTable(IEnumerable<OrderItemEmail> items)
        {
            string tableBody = "";
            int count = 1;
            float total = 0;
            foreach (var item in items)
            {
                string row = "<tr style=\"background - color: white; border - bottom: 1px solid gray; \">" +
                                $"<td style = \"padding: 25px 5px;\" >{count++}</td>" +
                                $"<td>{item.ProductName} {item.VolumeValue}(L)</td>" +
                                $"<td>{item.UnitPrice}</td>" +
                                $"<td>{item.Quantity}</td >" +
                                $"<td><b>{item.UnitPrice * item.Quantity}</b></td >" +
                            "</tr> ";
                tableBody += row;
                total += item.UnitPrice * item.Quantity;
            }

            
            string totalRow = "<tr>" +
                "<td colspan = \"5\" style = \"text-align: center; padding: 25px 5px;\">" +
                       $"Total: <b>{total} VNĐ</b>" +
                      "</td>" +
                  "</tr> ";

            tableBody += totalRow;

            return tableBody;
        }

        public string GenerateAccountConfirmEmail(string confirmationToken)
        {
            var path = _env.WebRootPath + "\\Email_Templates\\account_confirm.html";
            string contents = "";

            using(var reader = File.OpenText(path))
            {
                contents = reader.ReadToEnd();
            }

            contents = string.Format(contents, confirmationToken);

            return contents;
        }

    }
}
