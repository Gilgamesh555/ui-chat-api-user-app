using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using UserApi.Dtos;

namespace UserApi.Models
{
    public class Contact
    {
        [JsonIgnore]
        [Key]
        public int Id { get; set; }
        public int UserSenderId { get; set; }
        public int UserReceiverId { get; set; }
        public string? chatId { get; set; }

        //Navigation
        [JsonIgnore]
        [ForeignKey("UserSenderId")]
        public User? UserContact { get; set; }
    }
}