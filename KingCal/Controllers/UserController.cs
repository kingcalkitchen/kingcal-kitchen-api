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
using System.Linq;
using System.Security.Claims;

namespace KingCal.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        private readonly IUser _userService;
        private readonly IUserRoles _userRolesService;
        private readonly AppSettings _appSettings;

        public UserController(ILogger<UserController> logger, IMapper mapper, IUser userService, IUserRoles userRolesService, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _mapper = mapper;
            _userService = userService;
            _userRolesService = userRolesService;
            _appSettings = appSettings.Value;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll() 
        {
            var users = _userService.GetAll();
            var usersDTO = _mapper.Map<IList<UserDTO>>(users);

            return Ok(usersDTO);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(Guid id) 
        {
            var user = _userService.GetById(id);
            var userDto = _mapper.Map<UserDTO>(user);
            return Ok(userDto);
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update(Guid id, [FromBody] UserDTO userDto) 
        {
            Guid currentUser = Guid.Empty;
            if (User.HasClaim(x => x.Type == ClaimTypes.Name))
                currentUser = Guid.Parse(User.Claims.Where(a => a.Type == ClaimTypes.Name).FirstOrDefault().Value);

            var user = _mapper.Map<User>(userDto);
            user.Id = id;

            try
            {
                _userService.Update(user, currentUser, userDto.Password);
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
            Guid currentUser = Guid.Empty;
            if (User.HasClaim(x => x.Type == ClaimTypes.Name))
                currentUser = Guid.Parse(User.Claims.Where(a => a.Type == ClaimTypes.Name).FirstOrDefault().Value);

            _userService.Delete(id, currentUser);
            return Ok();
        }
    }
}
