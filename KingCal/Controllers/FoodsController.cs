using KingCal.Data.DTOs;
using KingCal.Service.Interfaces;
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
    [ApiController]
    [Route("api/[controller]")]
    public class FoodsController : ControllerBase
    {
        private readonly ILogger<FoodsController> _logger;
        private readonly IFood _foodService;

        public FoodsController(ILogger<FoodsController> logger, IFood foodService)
        {
            _logger = logger;
            _foodService = foodService;
        }

        [HttpGet("GetAll")]
        public ActionResult<IAsyncEnumerable<FoodDTO>> GetAll()
        {
            IAsyncEnumerable<FoodDTO> list = _foodService.GetAllAsync();

            return Ok(list);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<FoodDTO>> GetById(Guid id) 
        {
            FoodDTO food = await _foodService.GetByIdAsync(id);

            if (food.Id == Guid.Empty)
                return NotFound();

            return Ok(food);
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create([FromBody] FoodDTO foodDTO) 
        {
            Guid id = await _foodService.CreateAsync(foodDTO);

            if (id == Guid.Empty)
                return StatusCode(303);

            return Created(
                new Uri(
                    string.Concat(Request.Path.ToString().Remove(Request.Path.ToString().Length - 6, 6), $"GetById/{id}"), 
                    UriKind.Relative), 
                id);
        }

        [HttpPut("Update")]
        public async Task<ActionResult> Update([FromBody] FoodDTO foodDTO) 
        {
            int response = await _foodService.UpdateAsync(foodDTO);

            if (response < 0)
                return NotFound();

            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(Guid id) 
        {
            int response = await _foodService.DeleteAsync(id);

            if (response > 0)
                return Ok();
            else if (response == -2)
                return StatusCode(410);
            else
                return NotFound();
        }
    }
}
