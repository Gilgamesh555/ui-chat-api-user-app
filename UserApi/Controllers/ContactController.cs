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
        private readonly ContactRequestService _contactRequestService;

        public ContactController(ContactService service, ContactRequestService contactRequestService)
        {
            _contactService = service;
            _contactRequestService = contactRequestService;
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
        public async Task<ActionResult<Contact>> Create(ContactDto contact)
        {
            var newContact = new Contact
            {
                UserSenderId = contact.UserSenderId,
                UserReceiverId = contact.UserReceiverId,
            };

            var createdContact = await _contactService.Create(newContact);

            return CreatedAtAction(nameof(GetContact), new { id = createdContact.Id }, createdContact);
        }

        // GET: api/contact/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<UserSearchRequest>>> GetContactRequestsByUser(int userId)
        {
            var contacts = await _contactRequestService.GetRequests(userId);

            return Ok(contacts);
        }

        // PUT: api/contact/{id}/status
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatusById(int id, [FromBody] MessageStatus status)
        {
            var contact = await _contactService.GetById(id);
            var updateStatus = await _contactRequestService.UpdateStatusById(contact.UserSenderId, contact.UserReceiverId, status);

            return Ok(updateStatus);
        }

        // Get if the user is already a contact or if there is a request pending
        // Get: api/contact/check-request/{userSenderId}/{userReceiverId}
        [HttpGet("check-request/{userSenderId}/{userReceiverId}")]
        public async Task<ActionResult<UserSearchRequest>> GetContactRequest(int userSenderId, int userReceiverId)
        {
            var contact = await _contactRequestService.CheckRequest(userSenderId, userReceiverId);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }
    }
}