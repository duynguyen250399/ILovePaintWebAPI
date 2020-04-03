using DataLayer.Entities;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.ColorService;
using System.Threading.Tasks;

namespace ILovePaintWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private readonly IColorService _colorService;

        public ColorsController(IColorService colorService)
        {
            _colorService = colorService;
        }

        [HttpPost]
        public async Task<IActionResult> AddColor(Color color)
        {
            if (color == null)
            {
                return BadRequest(new { message = "Color is null!" });
            }

            if (color.ProductID == 0)
            {
                return BadRequest(new { message = "Missing product id field!" });
            }

            var newColor = await _colorService.AddColorAsync(color);

            if (newColor == null)
            {
                return BadRequest(new { message = "Duplicated color" });
            }

            return Ok(new
            {
                status = "success",
                message = "Color added",
                data = newColor
            });
        }

        [HttpPost]
        [Route("many")]
        public async Task<IActionResult> AddColors(ColorsModel model)
        {
            if (model.Colors == null || model.Colors.Count == 0)
            {
                return BadRequest(new { message = "Color is null!" });
            }

            var newColors = await _colorService.AddColorsAsync(model.Colors);

            if (newColors == null)
            {
                return BadRequest(new { message = "Duplicate colors" });
            }

            return Ok(new
            {
                status = "success",
                message = "Colors added",
                data = newColors
            });
        }

        [HttpGet]
        [Route("{productId}")]
        public IActionResult GetColors(int productId)
        {
            var colors = _colorService.GetColors(productId);
            if (colors == null)
            {
                return NotFound(new { message = "Colors not found!" });
            }

            return Ok(colors);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteColor(int id)
        {
            var deletedColor = _colorService.DeleteColor(id);
            if (deletedColor == null)
            {
                return NotFound(new { message = "Color not found!" });
            }

            return Ok(new
            {
                status = "success",
                message = "Color deleted"
            });
        }

        [HttpPut]
        public IActionResult UpdateColor(Color color)
        {
            if (color == null)
            {
                return BadRequest(new { message = "Color is null!" });
            }

            if (color.ID == 0)
            {
                return BadRequest(new { message = "Missing color id!" });
            }

            var updatedColor = _colorService.UpdateColor(color);

            if (updatedColor == null)
            {
                return BadRequest(new
                {
                    message = "Duplicated color"
                });
            }

            return Ok(new
            {
                status = "success",
                message = "Color updated"
            });
        }


    }
}