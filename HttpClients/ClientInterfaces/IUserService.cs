using Domain.DTOs.UserDTO;
using Domain.Model;

namespace HttpClient.ClientInterfaces;

public interface IUserService
{
    Task<User> CreateAsync(UserCreationDTO userToCreate);
    Task<IEnumerable<User>> GetAsync(SearchUserParametersDTO searchParameters);
}