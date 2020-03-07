using AutoMapper;
using KingCal.Common.DTOs;
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
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetRolesByUserId(Guid id)
        {
            List<Role> roles = new List<Role>();

            IAsyncEnumerator<Role> role = _userRolesService.GetRolesByUserId(id).GetAsyncEnumerator();
            while (await role.MoveNextAsync()) roles.Add(role.Current);

            return Ok(_mapper.Map<List<RoleDTO>>(roles));
        }

        [HttpPost("AssignRole/{userId}/{roleId}")]
        public async Task<IActionResult> AssignRole(Guid userId, Guid roleId) 
        {
            Guid currentUser = Guid.Empty;
            if (User.HasClaim(x => x.Type == ClaimTypes.Name))
                currentUser = Guid.Parse(User.Claims.Where(a => a.Type == ClaimTypes.Name).FirstOrDefault().Value);

            UserRole userRole = await _userRolesService.AssignRole(userId, roleId, currentUser);

            return Ok();
        }

        [HttpPost("RemoveRole/{userId}/{roleId}")]
        public IActionResult RemoveRole(Guid userId, Guid roleId)
        {
            Guid currentUser = Guid.Empty;
            if (User.HasClaim(x => x.Type == ClaimTypes.Name))
                currentUser = Guid.Parse(User.Claims.Where(a => a.Type == ClaimTypes.Name).FirstOrDefault().Value);

            _userRolesService.RemoveRole(userId, roleId, currentUser);

            return Ok();
        }
    }
}
