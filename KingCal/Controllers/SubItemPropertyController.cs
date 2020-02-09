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
    public class SubItemPropertyController : ControllerBase
    {
        private readonly ILogger<AddressController> _logger;
        private readonly ISubItemProperty _subItemPropertyService;
        public SubItemPropertyController(ILogger<AddressController> logger, ISubItemProperty subItemPropertyService)
        {
            _logger = logger;
            _subItemPropertyService = subItemPropertyService;
        }

        [HttpGet, Route("GetAll")]
        public ActionResult<IAsyncEnumerable<SubItemPropertyDTO>> GetAll()
        {
            IAsyncEnumerable<SubItemPropertyDTO> list = _subItemPropertyService.GetAllAsync();

            return Ok(list);
        }

        [HttpGet, Route("GetById/{id}")]
        public async Task<ActionResult<SubItemPropertyDTO>> GetById(Guid id)
        {
            SubItemPropertyDTO subItemProperty = await _subItemPropertyService.GetByIdAsync(id);

            if (subItemProperty.Id == Guid.Empty)
                return NotFound();

            return Ok(subItemProperty);
        }

        [HttpPost, Route("Create")]
        public async Task<ActionResult> Create([FromBody] SubItemPropertyDTO subItemPropertyDTO)
        {
            Guid id = await _subItemPropertyService.CreateAsync(subItemPropertyDTO);

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

