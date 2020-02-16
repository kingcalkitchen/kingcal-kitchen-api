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
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategory _categoryService;

        public CategoryController(ILogger<CategoryController> logger, ICategory categoryService)
        {
            _logger = logger;
            _categoryService = categoryService;
        }

        [HttpGet, Route("GetAll")]
        public ActionResult<IAsyncEnumerable<CategoryDTO>> GetAll()
        {
            IAsyncEnumerable<CategoryDTO> list = _categoryService.GetAllAsync();

            return Ok(list);
        }

        [HttpGet, Route("GetById/{id}")]
        public async Task<ActionResult<CategoryDTO>> GetById(Guid id)
        {
            CategoryDTO category = await _categoryService.GetByIdAsync(id);

            if (category.Id == Guid.Empty)
                return NotFound();

            return Ok(category);
        }

        [HttpPost, Route("Create")]
        public async Task<ActionResult> Create([FromBody] CategoryDTO categoryDTO)
        {
            Guid id = await _categoryService.CreateAsync(categoryDTO);

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