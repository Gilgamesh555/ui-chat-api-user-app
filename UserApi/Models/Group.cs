using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UserApi.Models
{
    public class Group
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
        public ICollection<Chat>? Chats { get; set; }

        [JsonIgnore]
        public ICollection<UserGroup>? UserGroups { get; set; }

    }
}