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
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Authenticate([FromBody] UserDTO userDto) 
        {
            var user = _userService.Authenticate(userDto.Email, userDto.Password);

            if (user is null)
                return BadRequest("Email or password is incorrect.");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.SECRET);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    //new Claim(ClaimTypes.GivenName, user.LastName),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            IAsyncEnumerator<Role> role = _userRolesService.GetRolesByUserId(user.Id).GetAsyncEnumerator();
            while (await role.MoveNextAsync()) tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, role.Current.Name));

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { access_token = tokenString });
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register([FromBody] UserDTO userDto) 
        {
            Guid currentUser = Guid.Empty;
            if (User.HasClaim(x => x.Type == ClaimTypes.Name))
                currentUser = Guid.Parse(User.Claims.Where(a => a.Type == ClaimTypes.Name).FirstOrDefault().Value);

            var user = _mapper.Map<User>(userDto);

            try
            {
                User newUser = _userService.Create(user, userDto.Password, currentUser);
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
