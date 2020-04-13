using AutoMapper;
using Google.Apis.Auth;
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
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace KingCal.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IMapper _mapper;
        private readonly IUser _userService;
        private readonly IUserRoles _userRolesService;
        private readonly AppSettings _appSettings;

        public AuthController(ILogger<AuthController> logger, IMapper mapper, IUser userService, IUserRoles userRolesService, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _mapper = mapper;
            _userService = userService;
            _userRolesService = userRolesService;
            _appSettings = appSettings.Value;
        }
        private async Task<TokenDTO> GetToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.SECRET);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.GivenName, user.FirstName ?? string.Empty),
                    new Claim(ClaimTypes.Surname, user.LastName ?? string.Empty),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            IAsyncEnumerator<Role> role = _userRolesService.GetRolesByUserId(user.Id).GetAsyncEnumerator();
            while (await role.MoveNextAsync()) tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, role.Current.Name));

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new TokenDTO { access_token = tokenHandler.WriteToken(token) };
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Authenticate([FromBody] UserDTO userDto)
        {
            var user = _userService.Authenticate(userDto.Email, userDto.Password);

            if (user is null)
                return BadRequest("Email or password is incorrect.");

            return Ok(await GetToken(user));
        }

        [AllowAnonymous]
        [HttpPost("GoogleLogin")]
        public async Task<IActionResult> GoogleAuthenticate([FromBody]TokenDTO token)
        {
            var payload = ValidateAsync(token.access_token, new GoogleJsonWebSignature.ValidationSettings()).Result;
            var user = _userService.Authenticate(payload);

            return Ok(await GetToken(user));
        }

        [AllowAnonymous]
        [HttpPost("FacebookLogin")]
        public async Task<IActionResult> FacebookAuthenticate()
        {
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("OsLogin")]
        public async Task<IActionResult> OsAuthenticate()
        {
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword()
        {
            return Ok();
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
    }
}
