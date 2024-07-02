using Microsoft.AspNetCore.Mvc;
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

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(LoginRequest user)
        {
            try
            {
                var loggedUser = await _authService.Authenticate_User(user);
                return Ok(loggedUser);
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