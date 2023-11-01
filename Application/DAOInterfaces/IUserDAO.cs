using Domain.DTOs.UserDTO;
using Domain.Model;

namespace Application.DAOInterfaces;

public interface IUserDAO
{
    Task CreateAsync(User user);
    Task<User?> GetByUsernameAsync(string username);
    Task<IEnumerable<User>> GetAsync(SearchUserParametersDTO searchParameters);
}