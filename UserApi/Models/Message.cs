using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using UserApi.Dtos;

namespace UserApi.Models
{
    public class Message
    {
        [JsonIgnore]
        [Key]
        public int Id { get; set; }

        [Required]
        public int SenderId { get; set; }

        [Required]
        public int ChatId { get; set; }

        public MessageStatus? Status { get; set; }

        [JsonIgnore]
        public DateTime CreatedAt { get; set; }

        // Handling navigation
        [JsonIgnore]
        [ForeignKey("UserId")]
        public User? User { get; set; }

        [JsonIgnore]
        [ForeignKey("ChatId")]
        public Chat? Chat { get; set; }
    }
}