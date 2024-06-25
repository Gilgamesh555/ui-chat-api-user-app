using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UserApi.Models
{
    public class User
    {
        [JsonIgnore]
        [Key]
        public int Id { get; set; }

        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        [Required]
        public required string Email { get; set; }

        [Required]
        public required string Username { get; set; }

        [Required]
        public required string Password { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [JsonIgnore]
        public DateTime CreatedAt { get; set; }

        // Handling navigation
        [JsonIgnore]
        public ICollection<UserChat>? UserChats { get; set; }
    }
}