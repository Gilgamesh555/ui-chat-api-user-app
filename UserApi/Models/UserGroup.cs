using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using UserApi.Dtos;

namespace UserApi.Models
{
    public class UserGroup
    {
        [JsonIgnore]
        [Key, Column(Order = 0)]
        public int UserId { get; set; }
        [Key, Column(Order = 1)]
        public int GroupId { get; set; }
        public UserGroupStatus Status { get; set; }

        //Navigation
        [JsonIgnore]
        public User? User { get; set; }
        [JsonIgnore]
        public Group? Group { get; set; }
    }
}