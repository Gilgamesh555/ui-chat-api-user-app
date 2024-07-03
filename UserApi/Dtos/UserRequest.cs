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

        public int? ContactId { get; set; }
    }

    public class UserSearchChatRequest : UserSearchRequest
    {
        public required int UserSenderId { get; set; }
        public required int UserReceiverId { get; set; }
    }
}