using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Entities;
using ILovePaintWebAPI.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.ProductService;

namespace ILovePaintWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
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
                p.Image = Utils.ImagePathToLink(p.ID);
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

            product.Image = Utils.ImagePathToLink(product.ID);

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct([FromForm] Product newProduct)
        {

            if (newProduct == null)
            {
                return BadRequest("Product is null!");
            }

            return Ok(await _productService.AddProductAsync(newProduct));
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
        public IActionResult UpdateProduct([FromForm] Product product)
        {
            if (product == null)
            {
                return BadRequest("Invalid product!");
            }

            if (product.ID == 0)
            {
                return BadRequest("Missing Product ID field!");
            }

            Product oldProduct = _productService.GetProductById(product.ID);
            if (oldProduct == null)
            {
                return NotFound($"Product with id {product.ID} not found!");
            }

            if(product.Image == null)
            {
                product.Image = oldProduct.Image;
            }

            Product newProduct = _productService.UpdateProduct(product);

            return Ok(newProduct);

        }

    }

}