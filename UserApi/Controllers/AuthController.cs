using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using UserApi.Dtos;
using UserApi.Exceptions;
using UserApi.Models;
using UserApi.Service;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly TokenService _tokenService;

        public AuthController(AuthService authService, TokenService tokenService)
        {
            _authService = authService;
            _tokenService = tokenService;
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<ActionResult<UserLoggedIn>> Login(LoginRequest user)
        {
            try
            {
                var loggedUser = await _authService.Authenticate_User(user);

                var token = _tokenService.CreateToken(loggedUser);

                var userLoggedIn = new UserLoggedIn
                {
                    Id = loggedUser.Id,
                    Email = loggedUser.Email,
                    Username = loggedUser.Username,
                    Token = token
                };

                return Ok(userLoggedIn);
            }
            catch (UserNotFoundException e)
            {
                return NotFound(new { message = e.Message });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        // Validate Token
        // POST: api/auth/validate
        [HttpPost("validate")]
        [Authorize]
        public async Task<ActionResult<UserLoggedIn>> ValidateToken()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!_tokenService.ValidateToken(token))
            {
                return Unauthorized();
            }

            var userId = int.Parse(User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new Exception("User or Claim not found"));

            var user = await _authService.GetUserById(userId);

            if (user == null)
            {
                return NotFound();
            }

            var newUserLoggedIn = new UserLoggedIn
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Token = token
            };

            return Ok(newUserLoggedIn);
        }

        // GET: api/auth/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _authService.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}