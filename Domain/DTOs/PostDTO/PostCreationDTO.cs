namespace Domain.DTOs.PostDTO;

public class PostCreationDTO
{
    public string OwnerUsername { get; }
    public string? Title { get; }
    public string? Body { get; }
    

    public PostCreationDTO(string ownerUsername, string? title, string? body)
    {
        OwnerUsername = ownerUsername;
        Title = title;
        Body = body;
    }
}