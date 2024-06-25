using Microsoft.AspNetCore.Mvc;
using UserApi.Models;
using UserApi.Service;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserChatController : ControllerBase
    {
        private readonly UserChatService _userChatService;

        public UserChatController(UserChatService userChatService)
        {
            _userChatService = userChatService;
        }

        // GET: api/userchat
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userChatService.GetAll());
        }

        // GET: api/userchat/{userId}/{chatId}
        [HttpGet("{userId}/{chatId}")]
        public async Task<IActionResult> GetUserChatById(int userId, int chatId)
        {
            var userChat = await _userChatService.GetById(userId, chatId);

            if (userChat == null)
            {
                return NotFound();
            }

            return Ok(userChat);
        }

        // POST: api/userchat
        [HttpPost]
        public async Task<ActionResult<UserChat>> Post(UserChat userChat)
        {
            var createdUserChat = await _userChatService.Create(userChat);

            return CreatedAtAction(nameof(GetUserChatById), new { userId = createdUserChat.UserId, chatId = createdUserChat.ChatId }, createdUserChat);
        }
    }
}