using AutoMapper;
using KingCal.Common.DTOs;
using KingCal.Common.Helpers;
using KingCal.Common.Models;
using KingCal.Data.Entities;
using KingCal.Service.User.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace KingCal.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly ILogger<RolesController> _logger;
        private readonly IMapper _mapper;
        private readonly IRole _roleService;
        private readonly AppSettings _appSettings;

        public RolesController(ILogger<RolesController> logger, IMapper mapper, IRole roleService, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _mapper = mapper;
            _roleService = roleService;
            _appSettings = appSettings.Value;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var roles = _roleService.GetAll();
            var rolesDto = _mapper.Map<IList<RoleDTO>>(roles);

            return Ok(rolesDto);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(Guid id)
        {
            var role = _roleService.GetById(id);
            var roleDto = _mapper.Map<RoleDTO>(role);
            return Ok(roleDto);
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] RoleDTO roleDto) 
        {
            var role = _mapper.Map<Role>(roleDto);

            try
            {
                Role newRole = _roleService.Create(role);
                return Ok(newRole.Id);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update(Guid id, [FromBody] RoleDTO roleDto)
        {
            var role = _mapper.Map<Role>(roleDto);
            role.Id = id;

            try
            {
                _roleService.Update(role);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            _roleService.Delete(id);
            return Ok();
        }

    }
}
