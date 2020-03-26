using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.ImageService;
using ServiceLayer.ProductService;

namespace ILovePaintWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly IProductService _productService;

        public ImagesController(IWebHostEnvironment env, IProductService productService)
        {
            _env = env;
            _productService = productService;
        }

        [HttpGet]
        [Route("product/{productId}")]
        public IActionResult GetImage(int productId)
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
                imagePath = _env.WebRootPath + p.Image;
            }

            var imageFile = System.IO.File.OpenRead(imagePath);
            if (imageFile == null)
            {
                return BadRequest("Error while downloading file!");
            }

            return File(imageFile, "image/jpeg");

        }

        [HttpPost]
        [Route("product/{imageName}")]
        public async Task<ActionResult> PostProductImage(IFormFile file, string imageName)
        {
            if (file != null && file.Length > 0)
            {
                string subPath = $"\\uploads\\images\\products\\";
                string uploadPath = _env.WebRootPath + subPath;

                try
                {
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }

                    using (var stream = System.IO.File.Create(uploadPath + imageName))
                    {
                        await file.CopyToAsync(stream);
                        await stream.FlushAsync();
                    }

                    return Ok(new { message = "Image has been uploaded!"});
                }
                catch (Exception e)
                {
                    return Content(e.Message);
                }
            }
            else
            {
                return Content("Failed to upload file!");
            }
        }

        [HttpPut]
        [Route("product/{productId}/{imageName}")]
        public async Task<ActionResult> UpdateImage(IFormFile file, int productId, string imageName)
        {
            if (file != null && file.Length > 0)
            {
                Product p = _productService.GetProductById(productId);
                if (p == null)
                {
                    return NotFound("Product not found!");
                }

                string subPath = $"\\uploads\\images\\products\\";
                string path = _env.WebRootPath + subPath;

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                try
                {
                    if (!string.IsNullOrEmpty(p.Image))
                    {
                        System.IO.File.Delete(_env.WebRootPath + p.Image);
                    }

                    using (var stream = System.IO.File.Create(path + imageName))
                    {
                        await file.CopyToAsync(stream);
                        await stream.FlushAsync();
                    }

                    p.Image = (subPath + imageName).Replace("\\", "/");
                    _productService.UpdateProduct(p);

                    return Ok("Image uploaded...");
                }
                catch (Exception e)
                {
                    return Content(e.Message);
                }
                //return Content($"pro id: {productId} - image name: {imageName}");
            }
            else
            {
                return BadRequest("Please select file!");
            }
        }

        [HttpDelete]
        [Route("product/{imageUrl}")]
        public IActionResult DeleteImage(string imageUrl)
        {
            string path = _env.WebRootPath + "\\uploads\\images\\products\\" + imageUrl;

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                return Ok();
            }
            else
            {
                return NotFound("File not found!");
            }
            
        }
    }
}