namespace Domain.DTOs.UserDTO;

public class SearchUserParametersDTO
{
    public string? UsernameContains { get; }
    public string? UsernameMatches { get; }

    public SearchUserParametersDTO(string usernameContains, string usernameMatches)
    {
        UsernameContains = usernameContains;
        UsernameMatches = usernameMatches;
    }
}