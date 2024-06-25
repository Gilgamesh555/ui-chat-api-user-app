using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace UserApi.Models
{
    public class UserChat
    {
        [Required]
        [Key, Column(Order = 0)]
        public int UserId { get; set; }

        [Required]
        [Key, Column(Order = 1)]
        public int ChatId { get; set; }

        public string? Status { get; set; }

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