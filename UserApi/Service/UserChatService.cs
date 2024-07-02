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
            if (userChat.Status == null)
            {
                userChat.Status = Dtos.UserChatStatus.Pending;
            }

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

        public async Task<UserChat> HandleRequestInvitation(int userId, int chatId, Dtos.UserChatStatus status)
        {
            var userChat = await GetById(userId, chatId);

            if (userChat == null)
            {
                throw new Exception("UserChat not found");
            }

            userChat.Status = status;
            await _context.SaveChangesAsync();
            return userChat;
        }
    }
}