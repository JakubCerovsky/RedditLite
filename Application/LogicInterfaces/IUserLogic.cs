using Domain.DTO;
using Domain.Model;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    Task<User> CreateAsync(UserCreationDTO userToCreate);
}