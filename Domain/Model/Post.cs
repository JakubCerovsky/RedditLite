using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Model;

public class Post
{
    [Key]
    public int Id { get; set; }
    public User Owner { get; private set;}
    [ForeignKey("Owner")]
    public string OwnerUsername { get; set; }
    public string Title { get; private set; }
    public string Body { get; private set; }
    
    public Post(string ownerUsername, string title, string body)
    {
        OwnerUsername = ownerUsername;
        Title = title;
        Body = body;
    }
    private Post(){}
}