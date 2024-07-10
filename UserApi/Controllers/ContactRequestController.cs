using Microsoft.AspNetCore.Mvc;
using UserApi.Dtos;
using UserApi.Models;
using UserApi.Service;

namespace UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactRequestController : ControllerBase
    {
        private readonly ContactRequestService _contactRequestService;

        public ContactRequestController(ContactRequestService service)
        {
            _contactRequestService = service;
        }

        // GET: api/ContactRequest
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactRequest>>> GetContactRequests()
        {
            var contactRequests = await _contactRequestService.GetAll();
            return Ok(contactRequests);
        }

        // GET: api/ContactRequest/{senderId}/{receiverId}
        [HttpGet("{senderId}/{receiverId}")]
        public async Task<ActionResult<ContactRequest>> GetContactRequest(int senderId, int receiverId)
        {
            var contactRequest = await _contactRequestService.GetById(senderId, receiverId);

            if (contactRequest == null)
            {
                return NotFound();
            }

            return contactRequest;
        }

        // POST: api/ContactRequest
        [HttpPost]
        public async Task<ActionResult<ContactRequest>> Create(ContactRequest contactRequest)
        {
            var createdContactRequest = await _contactRequestService.Create(contactRequest);
            return CreatedAtAction(nameof(GetContactRequest), new { receiverId = createdContactRequest.UserReceiverId, senderId = createdContactRequest.UserSenderId }, createdContactRequest);
        }

        // POST api/ContactRequest/create/contact
        [HttpPost("create/contact")]
        public async Task<ActionResult<ContactRequestDto>> CreateContactRequest(ContactDto contactRequestDto)
        {
            var contactRequest = new ContactRequest
            {
                UserSenderId = contactRequestDto.UserSenderId,
                UserReceiverId = contactRequestDto.UserReceiverId,
                Status = contactRequestDto.Status
            };

            var createdContactRequest = await _contactRequestService.Create(contactRequest);

            var newContactRequestDto = new ContactRequestDto
            {
                Id = $"{createdContactRequest.UserSenderId}-{createdContactRequest.UserReceiverId}",
                UserSenderId = createdContactRequest.UserSenderId,
                UserReceiverId = createdContactRequest.UserReceiverId,
                Status = createdContactRequest.Status
            };

            return Ok(newContactRequestDto);
        }

        // GET: api/ContactRequest/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<UserSearchRequest>>> GetContactRequestsByUser(int userId)
        {
            var contactRequests = await _contactRequestService.GetRequests(userId);

            return Ok(contactRequests);
        }

        // PUT: api/ContactRequest/
        [HttpPut("{senderId}/{receiverId}/status")]
        public async Task<ActionResult<ContactRequest>> UpdateContactRequestStatus(int senderId, int receiverId, [FromBody] MessageStatus status)
        {
            var updatedContactRequest = await _contactRequestService.UpdateStatusById(senderId, receiverId, status);

            return Ok(updatedContactRequest);
        }
    }
}
