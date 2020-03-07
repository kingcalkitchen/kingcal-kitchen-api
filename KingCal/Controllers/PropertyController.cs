using KingCal.Common.DTOs;
using KingCal.Service.Item.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KingCal.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly ILogger<PropertyController> _logger;
        private readonly IProperty _propertyService;
        public PropertyController(ILogger<PropertyController> logger, IProperty propertyService)
        {
            _logger = logger;
            _propertyService = propertyService;
        }

        [HttpGet, Route("GetAll")]
        public ActionResult<IAsyncEnumerable<PropertyDTO>> GetAll()
        {
            IAsyncEnumerable<PropertyDTO> list = _propertyService.GetAllAsync();

            return Ok(list);
        }

        [HttpGet, Route("GetByItem/{itemId}")]
        public ActionResult<IAsyncEnumerable<PropertyDTO>> GetByItem(Guid itemId)
        {
            IAsyncEnumerable<PropertyDTO> list = _propertyService.GetByItemAsync(itemId);

            return Ok(list);
        }

        [HttpGet, Route("GetById/{id}")]
        public async Task<ActionResult<PropertyDTO>> GetById(Guid id)
        {
            PropertyDTO property = await _propertyService.GetByIdAsync(id);

            if (property.Id == Guid.Empty)
                return NotFound();

            return Ok(property);
        }

        [HttpPost, Route("Create")]
        public async Task<ActionResult> Create([FromBody] PropertyDTO propertyDTO)
        {
            Guid id = await _propertyService.CreateAsync(propertyDTO);

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

