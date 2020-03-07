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
    public class ItemSubItemController : ControllerBase
    {
        private readonly ILogger<ItemSubItemController> _logger;
        private readonly IItemSubItem _itemSubItemService;

        public ItemSubItemController(ILogger<ItemSubItemController> logger, IItemSubItem itemSubItemService)
        {
            _logger = logger;
            _itemSubItemService = itemSubItemService;
        }

        /*        [HttpGet, Route("GetAll")]
                public ActionResult<IAsyncEnumerable<ItemSubItemDTO>> GetAll()
                {
                    IAsyncEnumerable<ItemSubItemDTO> list = _itemSubItemService.GetAllAsync();

                    return Ok(list);
                }

                [HttpGet, Route("GetById/{id}")]
                public async Task<ActionResult<ItemSubItemDTO>> GetById(Guid id)
                {
                    ItemSubItemDTO itemSubItem = await _itemSubItemService.GetByIdAsync(id);

                    if (itemSubItem.Id == Guid.Empty)
                        return NotFound();

                    return Ok(itemSubItem);
                }
        */

        [HttpGet, Route("GetSubItemByItem/{itemId}")]
        public ActionResult<IAsyncEnumerable<ItemSubItemDTO>> GetSubItemByItem(Guid itemId)
        {
            IAsyncEnumerable<ItemSubItemDTO> list = _itemSubItemService.GetSubItemByItemAsync(itemId);

            return Ok(list);
        }


        [HttpPost, Route("AddSubItemToItem/{itemId}/{subItemId}")]
        public async Task<ActionResult> AddSubItemToItem(Guid itemId, Guid subItemId)
        {
            bool response = await _itemSubItemService.AddSubItemToItemAsync(itemId, subItemId);

            if (response == false)
                return StatusCode(303);

            return Ok("Add SubItem To Item - Success");
        }
    }
}