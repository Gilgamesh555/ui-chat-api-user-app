namespace UserApi.Dtos
{
    public class ContactDto
    {
        public int Id { get; set; }
        public int UserSenderId { get; set; }
        public int UserReceiverId { get; set; }
        public MessageStatus Status { get; set; }
    }
}