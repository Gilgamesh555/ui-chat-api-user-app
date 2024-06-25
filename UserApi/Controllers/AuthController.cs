using Microsoft.AspNetCore.Mvc;
using UserApi.Dtos;
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
            var loggedUser = await _authService.Authenticate_User(user);
            return CreatedAtAction(nameof(GetUserById), new { id = loggedUser.Id }, loggedUser);
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