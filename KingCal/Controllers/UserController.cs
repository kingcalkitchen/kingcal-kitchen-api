using AutoMapper;
using KingCal.Data.DTOs;
using KingCal.Data.Entities;
using KingCal.Data.Models;
using KingCal.Service.Helpers;
using KingCal.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

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

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] UserDTO userDto) 
        {
            var user = _userService.Authenticate(userDto.Username, userDto.Password);

            if (user is null)
                return BadRequest("Username or password is incorrect.");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.SECRET);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.GivenName, user.LastName),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            List<Role> roles = _userRolesService.GetRolesByUserId(user.Id).ToList();
            foreach (var role in roles)
            {
                tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, role.Name));
            }

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { access_token = tokenString });
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register([FromBody] UserDTO userDto) 
        {
            var user = _mapper.Map<User>(userDto);

            try
            {
                User newUser = _userService.Create(user, userDto.Password);
                return Ok(newUser.Id);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
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
            var user = _mapper.Map<User>(userDto);
            user.Id = id;

            try
            {
                _userService.Update(user, userDto.Password);
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
            _userService.Delete(id);
            return Ok();
        }
    }
}
