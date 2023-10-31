using System.Text.Json.Serialization;

namespace Domain.Model;

public class Post
{
    public int Id { get; set; }
    public string OwnerUsername { get; }
    public string Title { get; set; }
    public string Body { get; set; }
    
    public Post(string ownerUsername, string title, string body)
    {
        OwnerUsername = ownerUsername;
        Title = title;
        Body = body;
    }
}