using Domain.DTOs.UserDTO;
using Domain.Model;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    Task<User> CreateAsync(UserCreationDTO userToCreate);
    Task<IEnumerable<User>> GetAsync(SearchUserParametersDTO searchParameters);
    
}

