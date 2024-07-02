using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using UserApi.Dtos;

namespace UserApi.Models
{
    public class Contact
    {
        [JsonIgnore]
        [Key]
        public int Id { get; set; }
        public int UserContactId { get; set; }
        public MessageStatus Status { get; set; }

        //Navigation
        [JsonIgnore]
        public User? UserContact { get; set; }
    }
}