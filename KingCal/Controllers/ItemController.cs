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

        [HttpGet, Route("GetById/{id}")]
        public async Task<ActionResult<ItemDTO>> GetById(Guid id)
        {
            ItemDTO item = await _itemService.GetByIdAsync(id);

            if (item.Id == Guid.Empty)
                return NotFound();

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