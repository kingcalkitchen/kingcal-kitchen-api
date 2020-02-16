using KingCal.Data.DTOs;
using KingCal.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KingCal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly ILogger<AddressController> _logger;
        private readonly IAddress _addressService;

        public AddressController(ILogger<AddressController> logger, IAddress addressService)
        {
            _logger = logger;
            _addressService = addressService;
        }

        [HttpGet, Route("GetAll")]
        public ActionResult<IAsyncEnumerable<AddressDTO>> GetAll()
        {
            IAsyncEnumerable<AddressDTO> list = _addressService.GetAllAsync();

            return Ok(list);
        }

        [HttpGet, Route("GetByPostalCode/{postalCode}/{addressType}")]
        public ActionResult<IAsyncEnumerable<AddressDTO>> GetByPostalCode(int postalCode, int addressType)
        {
            IAsyncEnumerable<AddressDTO> list = _addressService.GetByPostalCodeAsync(postalCode, addressType);

            return Ok(list);
        }

        [HttpGet, Route("GetByCityState/{city}/{state}/{addressType}")]
        public ActionResult<IAsyncEnumerable<AddressDTO>> GetByCityState(string city, string state, int addressType)
        {
            IAsyncEnumerable<AddressDTO> list = _addressService.GetByCityStateAsync(city, state, addressType);

            return Ok(list);
        }


        [HttpGet, Route("GetById/{id}")]
        public async Task<ActionResult<AddressDTO>> GetById(Guid id)
        {
            AddressDTO address = await _addressService.GetByIdAsync(id);

            if (address.Id == Guid.Empty)
                return NotFound();

            return Ok(address);
        }

        [HttpPost, Route("Create")]
        public async Task<ActionResult> Create([FromBody] AddressDTO addressDTO)
        {
            Guid id = await _addressService.CreateAsync(addressDTO);

            if (id == Guid.Empty)
                return StatusCode(303);

            return Created(
                new Uri(
                    string.Concat(Request.Path.ToString().Remove(Request.Path.ToString().Length - 6, 6), $"GetById/{id}"),
                    UriKind.Relative),
                id);
        }

    }
}