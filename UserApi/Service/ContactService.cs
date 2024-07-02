using UserApi.Data;
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

        public Contact GetById(int id)
        {
            return _context.Contacts.Find(id) ?? throw new Exception("Contact not found");
        }

        public IEnumerable<Contact> GetAll()
        {
            return _context.Contacts.ToList();
        }
    }
}