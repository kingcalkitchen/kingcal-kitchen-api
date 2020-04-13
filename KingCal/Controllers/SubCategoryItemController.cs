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
    public class SubCategoryItemController : ControllerBase
    {
        private readonly ILogger<SubCategoryItemController> _logger;
        private readonly ISubCategoryItem _subCategoryItemService;

        public SubCategoryItemController(ILogger<SubCategoryItemController> logger, ISubCategoryItem subCategoryItemService)
        {
            _logger = logger;
            _subCategoryItemService = subCategoryItemService;
        }

        [HttpGet, Route("GetSubCategoryByItem/{itemId}")]
        public ActionResult<IAsyncEnumerable<SubCategoryItemDTO>> GetSubCategoryByItem(Guid itemId)
        {
            IAsyncEnumerable<SubCategoryItemDTO> list = _subCategoryItemService.GetSubCategoryByItemAsync(itemId);

            return Ok(list);
        }

        [HttpGet, Route("GetItemBySubCategory/{subCategoryId}")]
        public ActionResult<IAsyncEnumerable<SubCategoryItemDTO>> GetItemBySubCategory(Guid subCategoryId)
        {
            IAsyncEnumerable<SubCategoryItemDTO> list = _subCategoryItemService.GetItemBySubCategoryAsync(subCategoryId);

            return Ok(list);
        }

        [HttpPost, Route("AddItemToSubCategory/{subCategoryId}/{itemId}")]
        public async Task<ActionResult> AddItemToSubCategory(Guid subCategoryId, Guid itemId)
        {
            bool response = await _subCategoryItemService.AddItemToSubCategoryAsync(subCategoryId, itemId);

            if (response == false)
                return StatusCode(303);

            return Ok("Add Item To SubCategory - Success");
        }
    }
}