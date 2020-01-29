using AutoMapper;
using KingCal.Data.DTOs;
using KingCal.Data.Entities;
using KingCal.Data.Models;
using KingCal.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace KingCal.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class UserRolesController : ControllerBase
    {
        private readonly ILogger<UserRolesController> _logger;
        private readonly IMapper _mapper;
        private readonly IUserRoles _userRolesService;
        private readonly AppSettings _appSettings;

        public UserRolesController(ILogger<UserRolesController> logger, IMapper mapper, IUserRoles userRolesService, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _mapper = mapper;
            _userRolesService = userRolesService;
            _appSettings = appSettings.Value;
        }

        [HttpGet("{id}")]
        public IActionResult GetRolesByUserId(Guid id) 
        {
            return Ok(_mapper.Map<List<RoleDTO>>(_userRolesService.GetRolesByUserId(id).ToList()));
        }

        [HttpPost("AssignRole/{userId}/{roleId}")]
        public IActionResult AssignRole(Guid userId, Guid roleId) 
        {
            Guid currentUser = Guid.Empty;
            if (User != null) 
            {
                currentUser = Guid.Parse(User.Claims.Where(a => a.Type == ClaimTypes.Name).FirstOrDefault().Value);
            }

            UserRole userRole = _userRolesService.AssignRole(userId, roleId, currentUser);

            return Ok();
        }

        [HttpPost("RemoveRole/{userId}/{roleId}")]
        public IActionResult RemoveRole(Guid userId, Guid roleId)
        {
            Guid currentUser = Guid.Empty;
            if (User != null)
                currentUser = Guid.Parse(User.Claims.Where(a => a.Type == ClaimTypes.Name).FirstOrDefault().Value);

            _userRolesService.RemoveRole(userId, roleId, currentUser);

            return Ok();
        }
    }
}
