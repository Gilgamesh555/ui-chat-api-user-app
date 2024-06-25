using Microsoft.AspNetCore.Mvc;
using UserApi.Models;
using UserApi.Service;
using System.Collections.Generic;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        // GET: api/user
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userService.GetAll());
        }

        // GET: api/user/{id}
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            return Ok(_userService.GetById(id));
        }

        // POST: api/user
        [HttpPost]
        public async Task<ActionResult<User>> Post(User user)
        {
            user.Id = 0;
            var createdUser = await _userService.Create(user);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }

        // PUT: api/user/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, User user)
        {
            return Ok(_userService.Update(id, user));
        }

        // DELETE: api/user/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok();
        }
    }
}