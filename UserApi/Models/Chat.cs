using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace UserApi.Models
{
    public class Chat
    {
        [JsonIgnore]
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [JsonIgnore]
        public DateTime CreatedAt { get; set; }

        [JsonIgnore]
        // Handling navigation
        public ICollection<UserChat>? UserChats { get; set; }

    }
}