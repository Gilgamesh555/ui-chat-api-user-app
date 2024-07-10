using UserApi.Models;

namespace UserApi.Dtos
{
    public class LoginRequest
    {
        public required string UserCredentials { get; set; }
        public required string Password { get; set; }
    }

    public class UserChatRequests : User
    {
        public required int ChatId { get; set; }
        public required MessageStatus Status { get; set; }
    }

    public class UserLoggedIn
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Token { get; set; }
    }
}