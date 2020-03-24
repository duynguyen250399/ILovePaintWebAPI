using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.ShipperService;

namespace ILovePaintWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippersController : ControllerBase
    {
        private readonly IShipperService _shipperService;

        public ShippersController(IShipperService shipperService)
        {
            _shipperService = shipperService;
        }

        [HttpGet]
        public IActionResult GetShippers()
        {
            var shippers = _shipperService.GetAllShippers();
            if(shippers == null || shippers.ToList().Count == 0)
            {
                return NotFound(new { message = "Shippers not found!" });
            }

            return Ok(shippers);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetShipper(int id)
        {
            var shipper = _shipperService.GetShiperById(id);
            if(shipper == null)
            {
                return NotFound(new { message = "Shipper not found!" });
            }

            return Ok(shipper);
        }

        [HttpPost]
        public async Task<IActionResult> PostShipper(Shipper shipper)
        {
            if(shipper == null)
            {
                return BadRequest(new { message = "Invalid data!" });
            }

            var newShipper = await _shipperService.AddShipperAsync(shipper);

            return Ok(newShipper);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteShipper(int id)
        {
            var deletedShipper = _shipperService.RemoveShipper(id);
            if(deletedShipper == null)
            {
                return NotFound(new { message = "Shipper not found!" });
            }

            return Ok(deletedShipper);
        }

        [HttpPut]
        public IActionResult UpdateShipper(Shipper shipper)
        {
            if(shipper == null)
            {
                return BadRequest(new { message = "Invalid shipper data" });
            }

            if(shipper.ID == 0)
            {
                return BadRequest(new { message = "Missing shipper id" });
            }

            var updatedShipper = _shipperService.UpdateShipper(shipper);

            return Ok(updatedShipper);
        }
    }
}