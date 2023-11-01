using Domain.DTOs.UserDTO;
using Domain.Model;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    Task CreateAsync(UserCreationDTO userToCreate);
    Task<User> ValidateUserAsync(UserLoginDTO userLogin);
    Task<IEnumerable<User>> GetAsync(SearchUserParametersDTO searchParameters);
    Task<User> GetSingleAsync(SearchUserParametersDTO searchParameters);
    
}

