using Microsoft.AspNetCore.Mvc;
using UserApi.Models;
using UserApi.Service;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly ChatService _chatService;

        public ChatController(ChatService chatService)
        {
            _chatService = chatService;
        }

        // GET: api/chat
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_chatService.GetAll());
        }

        // GET: api/chat/{id}
        [HttpGet("{id}")]
        public IActionResult GetChatById(int id)
        {
            return Ok(_chatService.GetById(id));
        }

        // POST: api/chat
        [HttpPost]
        public async Task<ActionResult<Chat>> Post(Chat chat)
        {
            chat.Id = 0;
            var createdChat = await _chatService.Create(chat);
            return CreatedAtAction(nameof(GetChatById), new { id = createdChat.Id }, createdChat);
        }
    }
}