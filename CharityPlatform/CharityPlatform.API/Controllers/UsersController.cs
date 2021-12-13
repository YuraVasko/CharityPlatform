using System;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using CharityPlatform.API.Infrastructure;
using CharityPlatform.API.Models.Requests;
using CharityPlatform.API.Models.Responses;
using System.Linq;
using CharityPlatform.DAL.Models;
using System.Threading.Tasks;
using CharityPlatform.Integration.Commands;
using MediatR;
using CharityPlatform.Integration.Queries;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IMediator _mediator;

        public UsersController(
            IUserService userService,
            AuthenticationSettings authenticationSettings,
            IMediator mediator)
        {
            _userService = userService;
            _authenticationSettings = authenticationSettings;
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateUserRequest model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var donor = await _mediator.Send(new GetDonor { DonorId = user.Id });
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_authenticationSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Username.ToString()),
                    new Claim("DonorId", donor.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest model)
        {
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Username = model.Username
            };

            try
            {
                var result = _userService.Create(user, model.Password);
                var registerDonnorResult = await _mediator.Send(new CreateDonor(user.FirstName, user.LastName, user.FirstName, result.Id));
                
                return Ok(new UserDataResponse
                {
                    FirstName = result.FirstName,
                    LastName = result.LastName,
                    Id = result.Id,
                    Username = result.Username
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            var model = users.Select(user=> new UserDataResponse
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
                Username = user.Username
            });

            return Ok(model);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var user = _userService.GetById(id);
            var model = new UserDataResponse
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
                Username = user.Username
            };
            return Ok(model);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] UpdateUserRequest model)
        {
            var user = new User 
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Username = model.Username
            };
            user.Id = id;

            try
            {
                _userService.Update(user, model.Password);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok();
        }
    }
}