using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Entities;
using DataLayer.Models;
using ILovePaintWebAPI.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ServiceLayer.ProductService;

namespace ILovePaintWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;

        public ProductsController(IProductService productService, IWebHostEnvironment env, IConfiguration configuration)
        {
            _productService = productService;
            _env = env;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {

            var products = _productService.GetProducts();
            if (products.ToList().Count == 0)
            {
                return NotFound("Products not found!");
            }
            foreach (var p in products)
            {
                p.Image = _configuration["backendEnv:host"] + "/api/products/images/" + p.ID;
            }

            return Ok(products);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound($"Product with id {id} not found!");
            }

            product.Image = _configuration["backendEnv:host"] + "/api/products/images/" + product.ID;

            return Ok(product);
        }

        [HttpGet]
        [Route("images/{productId}")]
        public IActionResult GetProductImage(int productId)
        {
            Product p = _productService.GetProductById(productId);
            string imagePath = null;
            if (p == null)
            {
                return NotFound("Image not found!");
            }

            if (string.IsNullOrEmpty(p.Image))
            {
                imagePath = _env.WebRootPath + "\\uploads\\images\\products\\default_product.png";
            }
            else
            {
                imagePath = _env.WebRootPath + p.Image.Replace("/", "\\");
            }

            var imageFile = System.IO.File.OpenRead(imagePath);
            if (imageFile == null)
            {
                return BadRequest("Error while downloading file!");
            }

            return File(imageFile, "image/jpeg");

        }

        [HttpPost]
        public async Task<ActionResult> AddProduct([FromForm] ProductModel model)
        {

            if (model == null)
            {
                return BadRequest("Product is null!");
            }

            if (model.CategoryID == 0 || model.ProviderID == 0)
            {
                return BadRequest("Missing category or provider id!");
            }


            var product = new Product
            {
                Name = model.ProductName,
                CategoryID = model.CategoryID,
                ProviderID = model.ProviderID,
                Description = model.Description
            };

            if (model.Image != null && model.Image.Length > 0)
            {
                string uuid = System.Guid.NewGuid().ToString();
                string imagePath = $"/uploads/images/products/{product.CategoryID}-{product.ProviderID}" +
                    $"-iLovePaint-{uuid}-{model.Image.FileName}";

                if (!Directory.Exists(_env.WebRootPath + @"\uploads\images\products\"))
                {
                    Directory.CreateDirectory(_env.WebRootPath + @"\uploads\images\products\");
                }

                using (var stream = System.IO.File.Create(_env.WebRootPath + imagePath.Replace("/", "\\")))
                {
                    await model.Image.CopyToAsync(stream);
                    await stream.FlushAsync();
                }

                product.Image = imagePath;
            }

            var p = await _productService.AddProductAsync(product);

            return Ok(p);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var product = _productService.DeleteProduct(id);
            if (product == null)
            {
                return BadRequest("Product is null!");
            }

            return Ok(product);
        }

        [HttpPut]
        [Route("{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromForm] ProductModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid product!");
            }

            var product = _productService.GetProductById(productId);
            if (product == null)
            {
                return NotFound(new { message = "Product not found!" });
            }

            product.Name = model.ProductName;
            product.CategoryID = model.CategoryID;
            product.ProviderID = model.ProviderID;
            product.Description = model.Description;

            if (model.Image != null && model.Image.Length > 0)
            {
                string uuid = System.Guid.NewGuid().ToString();
                string imagePath = $"/uploads/images/products/{product.CategoryID}-{product.ProviderID}" +
                    $"-iLovePaint-{uuid}-{model.Image.FileName}";

                if (!Directory.Exists(_env.WebRootPath + @"\uploads\images\products\"))
                {
                    Directory.CreateDirectory(_env.WebRootPath + @"\uploads\images\products\");
                }

                if (!string.IsNullOrEmpty(product.Image) && System.IO.File.Exists(_env.WebRootPath + product.Image.Replace("/", "\\"))){
                    System.IO.File.Delete(_env.WebRootPath + product.Image.Replace("/", "\\"));
                }

                using (var stream = System.IO.File.Create(_env.WebRootPath + imagePath.Replace("/", "\\")))
                {
                    await model.Image.CopyToAsync(stream);
                    await stream.FlushAsync();
                }

                product.Image = imagePath;
            }

            Product newProduct = _productService.UpdateProduct(product);

            return Ok(newProduct);

        }
    }
}