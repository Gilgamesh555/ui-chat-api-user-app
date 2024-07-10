using UserApi.Models;

namespace UserApi.Dtos
{
    public class UserSearchRequest
    {
        public required int Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required MessageStatus Status { get; set; }

        public int? UserSenderId { get; set; }
        public int? UserReceiverId { get; set; }
    }

    public class UserSearchChatRequest
    {
        public required int Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public MessageStatus? Status { get; set; }

        public int? UserSenderId { get; set; }
        public int? UserReceiverId { get; set; }
    }
}