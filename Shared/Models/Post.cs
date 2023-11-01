namespace Shared.Models;

public class Post
{
    public int Id { get; set; }
    public string OwnerUsername { get; }
    public string Title { get; set; }
    public string Body { get; set; }
}