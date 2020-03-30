using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class ProductModel
    {
        public string ProductName { get; set; }
        public int CategoryID { get; set; }
        public int ProviderID { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
    }
}
