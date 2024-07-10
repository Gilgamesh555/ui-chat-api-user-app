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
    }
}