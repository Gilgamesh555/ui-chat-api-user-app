namespace UserApi.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using UserApi.Dtos;
    using UserApi.Models;
    using UserApi.Service;

    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ContactService _contactService;

        public ContactController(ContactService service)
        {
            _contactService = service;
        }

        // GET: api/contact
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
        {
            var contacts = await _contactService.GetAll();
            return Ok(contacts);
        }

        // GET: api/contact/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContact(int id)
        {
            var contact = await _contactService.GetById(id);

            if (contact == null)
            {
                return NotFound();
            }

            return contact;
        }

        // POST: api/contact
        [HttpPost]
        public async Task<ActionResult<Contact>> Create(Contact contact)
        {
            Console.WriteLine(contact.UserReceiverId);
            var createdContact = await _contactService.Create(contact);
            return CreatedAtAction(nameof(GetContact), new { id = createdContact.Id }, createdContact);
        }

        // GET: api/contact/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<UserSearchRequest>>> GetContactsByUser(int userId)
        {
            var contacts = await _contactService.GetRequests(userId);

            return Ok(contacts);
        }

        // PUT: api/contact/{id}/status
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatusById(int id, [FromBody] MessageStatus status)
        {
            var contact = await _contactService.GetById(id);

            if (contact == null)
            {
                return NotFound();
            }

            await _contactService.UpdateStatusById(id, status);

            return Ok(status);
        }
    }
}