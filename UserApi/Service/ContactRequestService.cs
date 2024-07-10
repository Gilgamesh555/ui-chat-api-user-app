using Microsoft.EntityFrameworkCore;
using UserApi.Data;
using UserApi.Dtos;
using UserApi.Models;

namespace UserApi.Service
{
    public class ContactRequestService
    {
        private readonly UserApiDbContext _context;

        // Constructor
        public ContactRequestService(UserApiDbContext context)
        {
            _context = context;
        }

        // Example method GetAll
        public async Task<IEnumerable<ContactRequest>> GetAll()
        {
            return await _context.ContactRequests.ToListAsync();
        }

        // Example method GetById
        public async Task<ContactRequest> GetById(int senderId, int userReceiverId)
        {
            var contactRequest = await _context.ContactRequests.FindAsync(senderId, userReceiverId);

            if (contactRequest == null)
            {
                throw new Exception("Contact request not found");
            }

            return contactRequest;
        }

        // Example method Create
        public async Task<ContactRequest> Create(ContactRequest contactRequest)
        {
            _context.ContactRequests.Add(contactRequest);
            await _context.SaveChangesAsync();
            return contactRequest;
        }

        // Example method GetRequests
        public async Task<List<UserSearchRequest>> GetRequests(int userId)
        {
            var contactRequests = await _context.ContactRequests
                                             .Where(c => c.UserReceiverId == userId && c.Status != MessageStatus.Unsent)
                                             .ToListAsync();

            if (contactRequests == null)
            {
                throw new Exception("Contact requests not found");
            }

            var requests = new List<UserSearchRequest>();

            foreach (var contactRequest in contactRequests)
            {
                var user = await _context.Users.FindAsync(contactRequest.UserSenderId);

                if (user == null)
                {
                    throw new Exception("User not found");
                }

                requests.Add(new UserSearchRequest
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    Status = contactRequest.Status,
                    Email = user.Email,
                    UserSenderId = contactRequest.UserSenderId,
                    UserReceiverId = contactRequest.UserReceiverId
                });
            }

            return requests;
        }

        // Example method UpdateStatusById
        public async Task<ContactRequest> UpdateStatusById(int userId, int userReceiverId, MessageStatus status)
        {
            var contactRequest = await _context.ContactRequests.FindAsync(userId, userReceiverId);

            if (contactRequest == null)
            {
                throw new Exception("Contact request not found");
            }

            contactRequest.Status = status;

            await _context.SaveChangesAsync();

            return contactRequest;
        }
    }
}