using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.ProductVolumeService;

namespace ILovePaintWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductVolumesController : ControllerBase
    {
        private readonly IProductVolumeService _productVolumeService;

        public ProductVolumesController(IProductVolumeService productVolumeService)
        {
            _productVolumeService = productVolumeService;
        }

        [HttpPost]
        public IActionResult PostProductVolume(ProductVolume productVolume)
        {
            if(productVolume == null)
            {
                return BadRequest(new { message = "Product Volume info is invalid!" });
            }

            var newProductVolume = _productVolumeService.AddProductVolume(productVolume);

            if(newProductVolume == null)
            {
                return BadRequest(new { message = "This product volume is already existed!" });
            }

            return Ok(new { 
                message = "Product volume added",
                data = newProductVolume
            });
        }

        [HttpPut]
        public IActionResult UpdateProductVolume(ProductVolume productVolume)
        {
            if(productVolume == null)
            {
                return BadRequest(new { message = "Product volume is null" });
            }

            var updatedProductVolume = _productVolumeService.EditProductVolume(productVolume);

            return Ok(new
            {
                message = "Product volume has been updated",
                data = updatedProductVolume
            });
        }


    }
}