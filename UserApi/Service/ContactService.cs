using Microsoft.EntityFrameworkCore;
using UserApi.Data;
using UserApi.Dtos;
using UserApi.Models;

namespace UserApi.Service
{
    public class ContactService
    {
        private readonly UserApiDbContext _context;

        public ContactService(UserApiDbContext context)
        {
            _context = context;
        }

        public async Task<Contact> Create(Contact contact)
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return contact;
        }

        public async Task<Contact> GetById(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                throw new Exception("Contact not found");
            }
            return contact;
        }

        public async Task<IEnumerable<Contact>> GetAll()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task<List<UserSearchRequest>> GetRequests(int userId)
        {
            var contacts = await _context.Contacts
                                         .Where(c => c.UserReceiverId == userId && c.Status != MessageStatus.Unsent)
                                         .ToListAsync();

            if (contacts == null)
            {
                throw new Exception("Contacts not found");
            }

            var requests = new List<UserSearchRequest>();

            foreach (var contact in contacts)
            {
                var user = await _context.Users.FindAsync(contact.UserSenderId);

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
                    Email = user.Email,
                    Status = contact.Status,
                    ContactId = contact.Id
                });
            }

            return requests;
        }

        public async Task<Contact> UpdateStatusById(int id, MessageStatus status)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                throw new Exception("Contact not found");
            }

            contact.Status = status;

            await _context.SaveChangesAsync();

            return contact;
        }
    }
}