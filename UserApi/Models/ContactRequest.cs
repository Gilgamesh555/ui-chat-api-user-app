using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UserApi.Dtos;

namespace UserApi.Models
{
    public class ContactRequest
    {
        public int UserSenderId { get; set; }
        public int UserReceiverId { get; set; }
        public MessageStatus Status { get; set; }

        [ForeignKey("UserSenderId")]
        public User? UserSender { get; set; }

        [ForeignKey("UserReceiverId")]
        public User? UserReceiver { get; set; }
    }
}