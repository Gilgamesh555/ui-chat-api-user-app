using Microsoft.AspNetCore.Mvc;
using UserApi.Models;
using UserApi.Service;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly MessageService _MessageService;

        public MessageController(MessageService userChatService)
        {
            _MessageService = userChatService;
        }

        // GET: api/message
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_MessageService.GetAll());
        }

        // GET: api/message/{userId}/{chatId}
        [HttpGet("{userId}/{chatId}")]
        public async Task<IActionResult> GetMessageById(int userId, int chatId)
        {
            var message = await _MessageService.GetById(userId, chatId);

            if (message == null)
            {
                return NotFound();
            }

            return Ok(message);
        }

        // POST: api/message
        [HttpPost]
        public async Task<ActionResult<Message>> Post(Message message)
        {
            var createdMessage = await _MessageService.Create(message);

            return CreatedAtAction(nameof(GetMessageById), new { userId = createdMessage.SenderId, chatId = createdMessage.ChatId }, createdMessage);
        }

        // POST: api/message/{userId}/{chatId}/{status}
        [HttpPost("{userId}/{chatId}/{status}")]
        public async Task<IActionResult> HandleRequestInvitation(int userId, int chatId, Dtos.MessageStatus status)
        {
            try
            {
                var message = await _MessageService.HandleRequestInvitation(userId, chatId, status);
                return Ok(message);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
    }
}