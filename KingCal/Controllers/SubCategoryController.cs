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
    public class SubCategoryController : ControllerBase
    {
        private readonly ILogger<SubCategoryController> _logger;
        private readonly ISubCategory _subSubCategoryService;

        public SubCategoryController(ILogger<SubCategoryController> logger, ISubCategory subSubCategoryService)
        {
            _logger = logger;
            _subSubCategoryService = subSubCategoryService;
        }

        [HttpGet, Route("GetAll")]
        public ActionResult<IAsyncEnumerable<SubCategoryDTO>> GetAll()
        {
            IAsyncEnumerable<SubCategoryDTO> list = _subSubCategoryService.GetAllAsync();

            return Ok(list);
        }

        [HttpGet, Route("GetByCategory/{categoryId}")]
        public ActionResult<IAsyncEnumerable<SubCategoryDTO>> GetByCategory(Guid categoryId)
        {
            IAsyncEnumerable<SubCategoryDTO> list = _subSubCategoryService.GetByCategoryAsync(categoryId);

            return Ok(list);
        }

        [HttpGet, Route("GetById/{id}")]
        public async Task<ActionResult<SubCategoryDTO>> GetById(Guid id)
        {
            SubCategoryDTO subSubCategory = await _subSubCategoryService.GetByIdAsync(id);

            if (subSubCategory.Id == Guid.Empty)
                return NotFound();

            return Ok(subSubCategory);
        }

        [HttpPost, Route("Create")]
        public async Task<ActionResult> Create([FromBody] SubCategoryDTO subSubCategoryDTO)
        {
            Guid id = await _subSubCategoryService.CreateAsync(subSubCategoryDTO);

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