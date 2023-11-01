using Application.DAOInterfaces;
using Domain.DTOs.UserDTO;
using Domain.Model;

namespace FileData.DAO;

public class UserFileDAO:IUserDAO
{
    private readonly FileContext context;

    public UserFileDAO(FileContext context)
    {
        this.context = context;
    }

    public Task CreateAsync(User user)
    {

        context.Users.Add(user);
        context.SaveChanges();

        return Task.CompletedTask;
    }

    public Task<User?> GetByUsernameAsync(string username)
    {
        return Task.FromResult(context.Users.FirstOrDefault(u => 
            u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)));
    }

    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDTO searchParameters)
    {
        IEnumerable<User> users = context.Users.AsEnumerable();
        if (searchParameters.UsernameContains != null)
        {
            users = context.Users.Where(u => u.Username.Contains(searchParameters.UsernameContains, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(users);
    }
}