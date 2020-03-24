using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.ProviderService;

namespace ILovePaintWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvidersController : ControllerBase
    {

        private readonly IProviderService _providerService;

        public ProvidersController(IProviderService providerService)
        {
            _providerService = providerService;
        }

        [HttpGet]
        public IActionResult GetProviders()
        {
            var providers = _providerService.GetAllProviders();
            if(providers.ToList().Count == 0)
            {
                return NotFound("Providers not found!");
            }

            return Ok(providers);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetProvider(int id)
        {
            var provider = _providerService.GetProviderById(id);
            if(provider == null)
            {
                return NotFound($"Provider with id {id} not found!");
            }

            return Ok(provider);
        }

        [HttpPost]
        public async Task<ActionResult> PostProvider(Provider provider)
        {
            if(provider == null)
            {
                return BadRequest("Provider is invalid!");
            }

            return Ok(await _providerService.AddProvider(provider));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteProvider(int id)
        {
            var provider = await _providerService.DeleteProvider(id);
            if(provider == null)
            {
                return NotFound($"Provider with id {provider.ID} not found!");
            }

            return Ok(provider);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProvider(Provider provider)
        {
            if(provider == null)
            {
                return BadRequest("Invalid provider!");
            } 

            if(provider.ID == 0)
            {
                return BadRequest("Missing provider ID field!");
            }

            var oldProvider = _providerService.GetProviderById(provider.ID);
            
            if(oldProvider == null)
            {
                return NotFound($"Provider with id {provider.ID} not found!");
            }

            var newProvider = await _providerService.UpdateProvider(provider);

            return Ok(newProvider);
        }
    }
}