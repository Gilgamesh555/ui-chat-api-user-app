namespace UserApi.Dtos
{
    public class ContactRequestDto
    {
        public string? Id { get; set; }
        public int UserSenderId { get; set; }
        public int UserReceiverId { get; set; }
        public MessageStatus Status { get; set; }
    }
}