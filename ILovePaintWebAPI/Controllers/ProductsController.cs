using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
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
            if(products.ToList().Count == 0)
            {
                return NotFound("Products not found!");
            }

            return Ok(products);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetProductById(int id)
        {           
            var product = _productService.GetProductById(id);
            if(product == null)
            {
                return NotFound($"Product with id {id} not found!");
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct(Product newProduct)
        {
            if(newProduct == null)
            {
                return BadRequest("Product is null!");
            }

            return Ok(await _productService.AddProduct(newProduct));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _productService.DeleteProduct(id);
            if (product == null)
            {
                return BadRequest("Product is null!");
            }

            return Ok(product);
        }

        [HttpPut]   
        public async Task<ActionResult> UpdateProduct(Product product)
        {
            if(product == null)
            {
                return BadRequest("Invalid product!");
            }

            if(product.ID == 0)
            {
                return BadRequest("Missing Product ID field!");
            }

            Product oldProduct = _productService.GetProductById(product.ID);
            if(oldProduct == null)
            {
                return NotFound($"Product with id {product.ID} not found!");
            }

            Product newProduct = await _productService.UpdateProduct(product);
          
            return Ok(newProduct);

        }

    }

}