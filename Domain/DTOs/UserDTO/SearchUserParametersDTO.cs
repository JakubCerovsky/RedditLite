namespace Domain.DTOs.UserDTO;

public class SearchUserParametersDTO
{
    public string? UsernameContains { get; }

    public SearchUserParametersDTO(string usernameContains)
    {
        UsernameContains = usernameContains;
    }
}