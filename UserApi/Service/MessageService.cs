using UserApi.Data;
using UserApi.Models;

namespace UserApi.Service
{
    public class MessageService
    {
        private readonly UserApiDbContext _context;

        public MessageService(UserApiDbContext context)
        {
            _context = context;
        }

        public async Task<Message> Create(Message userChat)
        {
            if (userChat.Status == null)
            {
                userChat.Status = Dtos.MessageStatus.Unsent;
            }

            _context.Messages.Add(userChat);
            await _context.SaveChangesAsync();
            return userChat;
        }

        public async Task<Message> GetById(int userId, int chatId)
        {
            var userChat = await _context.Messages.FindAsync(userId, chatId);
            return userChat ?? throw new Exception("UserChat not found");
        }

        public IEnumerable<Message> GetAll()
        {
            return _context.Messages.ToList();
        }

        public async Task<Message> HandleRequestInvitation(int userId, int chatId, Dtos.MessageStatus status)
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