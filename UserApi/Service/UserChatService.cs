using UserApi.Data;
using UserApi.Models;

namespace UserApi.Service
{
    public class UserChatService
    {
        private readonly UserApiDbContext _context;

        public UserChatService(UserApiDbContext context)
        {
            _context = context;
        }

        public async Task<UserChat> Create(UserChat userChat)
        {
            _context.UserChats.Add(userChat);
            await _context.SaveChangesAsync();
            return userChat;
        }

        public async Task<UserChat> GetById(int userId, int chatId)
        {
            var userChat = await _context.UserChats.FindAsync(userId, chatId);
            return userChat ?? throw new Exception("UserChat not found");
        }

        public IEnumerable<UserChat> GetAll()
        {
            return _context.UserChats.ToList();
        }
    }
}