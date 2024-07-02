namespace UserApi.Dtos
{
    public class LoginRequest
    {
        public required string UserCredentials { get; set; }
        public required string Password { get; set; }
    }
}