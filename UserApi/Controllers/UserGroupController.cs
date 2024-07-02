namespace UserApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using UserApi.Data;
    using UserApi.Dtos;
    using UserApi.Models;

    [Route("api/[controller]")]
    [ApiController]
    public class UserGroupController : ControllerBase
    {
        private readonly UserApiDbContext _context;

        public UserGroupController(UserApiDbContext context)
        {
            _context = context;
        }

        // GET: api/UserGroup
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserGroup>>> GetUserGroup()
        {
            return await _context.UserGroups.ToListAsync();
        }

        // GET: api/UserGroup/{userId}/{groupId}
        [HttpGet("{id}/{groupId}")]
        public async Task<ActionResult<UserGroup>> GetUserGroup(int userId, int groupId)
        {
            var userGroup = await _context.UserGroups.FirstOrDefaultAsync(ug => ug.UserId == userId && ug.GroupId == groupId);

            if (userGroup == null)
            {
                return NotFound();
            }

            return userGroup;
        }

        // POST: api/UserGroup
        [HttpPost]
        public async Task<ActionResult<UserGroup?>> CreateUserGroup(UserGroup userGroup)
        {
            _context.UserGroups.Add(userGroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserGroup), new { userId = userGroup.UserId, groupId = userGroup.GroupId }, userGroup);
        }
    }
}