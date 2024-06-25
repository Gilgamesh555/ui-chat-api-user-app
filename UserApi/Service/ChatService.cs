using UserApi.Data;
using UserApi.Models;

namespace UserApi.Service
{
    public class ChatService
    {
        private readonly UserApiDbContext _context;

        public ChatService(UserApiDbContext context)
        {
            _context = context;
        }

        public async Task<Chat> Create(Chat chat)
        {
            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();
            return chat;
        }

        public Chat GetById(int id)
        {
            return _context.Chats.Find(id) ?? throw new Exception("Chat not found");
        }

        public IEnumerable<Chat> GetAll()
        {
            return _context.Chats.ToList();
        }
    }
}