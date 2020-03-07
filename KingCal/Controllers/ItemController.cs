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
    public class ItemController : ControllerBase
    {
        private readonly ILogger<ItemController> _logger;
        private readonly IItem _itemService;

        public ItemController(ILogger<ItemController> logger, IItem itemService)
        {
            _logger = logger;
            _itemService = itemService;
        }

        [HttpGet, Route("GetAll")]
        public ActionResult<IAsyncEnumerable<ItemDTO>> GetAll()
        {
            IAsyncEnumerable<ItemDTO> list = _itemService.GetAllAsync();

            return Ok(list);
        }

        [HttpGet, Route("GetAllWithSubType")]
        public ActionResult<IAsyncEnumerable<ItemDTO>> GetAllWithSubType()
        {
            IAsyncEnumerable<ItemDTO> list = _itemService.GetAllWithSubTypeAsync();

            return Ok(list);
        }

        [HttpGet, Route("GetById/{id}")]
        public ActionResult<IAsyncEnumerable<ItemDTO>> GetById(Guid id)
        {
            IAsyncEnumerable<ItemDTO> item = _itemService.GetByIdAsync(id);

            return Ok(item);
        }

        [HttpPost, Route("Create")]
        public async Task<ActionResult> Create([FromBody] ItemDTO itemDTO)
        {
            Guid id = await _itemService.CreateAsync(itemDTO);

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