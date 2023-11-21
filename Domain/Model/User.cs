using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Model;

public class User
{
    [Key]
    public string Username { get; set; }
    public string Password { get; set; }
    [JsonIgnore]
    public ICollection<Post> Posts { get; set; }
}