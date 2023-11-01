using System.Security.Claims;
using Domain.DTOs.UserDTO;
using Domain.Model;

namespace HttpClient.ClientInterfaces;

public interface IUserService
{
    Task LoginAsync(string username, string password);
    Task LogoutAsync();
    Task CreateAsync(UserCreationDTO userToCreate);
    Task<IEnumerable<User>> GetAsync(SearchUserParametersDTO searchParameters);
    Task<User> GetSingleAsync(SearchUserParametersDTO searchParameters);
    Task<ClaimsPrincipal> GetAuthAsync();

    Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
}