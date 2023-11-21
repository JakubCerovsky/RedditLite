namespace Domain.DTOs.PostDTO;

public class SearchPostParametersDTO
{
    public string? OwnerUsername { get; }

    public SearchPostParametersDTO(string? ownerUsername)
    {
        OwnerUsername = ownerUsername;
    }

}