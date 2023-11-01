using System.Text.RegularExpressions;
using Application.DAOInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs.UserDTO;
using Domain.Model;

namespace Application.Logic;

public class UserLogic:IUserLogic
{
    private readonly IUserDAO userDAO;

    public UserLogic(IUserDAO userDAO)
    {
        this.userDAO = userDAO;
    }
    
    public async Task CreateAsync(UserCreationDTO userToCreate)
    {
        User? existing = await userDAO.GetByUsernameAsync(userToCreate.Username);
        if (existing!=null)
        {
            throw new Exception("Username already taken!");
        }
        
        ValidateData(userToCreate);
        
        User toCreate = new User
        {
            Username = userToCreate.Username,
            Password = userToCreate.Password
        };
    
        await userDAO.CreateAsync(toCreate);
    }

    public async Task<User> ValidateUserAsync(UserLoginDTO userLogin)
    {
        User? existingUser = await userDAO.GetByUsernameAsync(userLogin.Username);
        
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.Password.Equals(userLogin.Password))
        {
            throw new Exception("Password mismatch");
        }

        return existingUser;
    }

    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDTO searchParameters)
    {
        return userDAO.GetAsync(searchParameters);
    }

    public Task<User> GetSingleAsync(SearchUserParametersDTO searchParameters)
    {
        return userDAO.GetByUsernameAsync(searchParameters.UsernameMatches);
    }

    private static void ValidateData(UserCreationDTO userCreationDTO)
    {
        ValidateUsername(userCreationDTO.Username);
        ValidatePassword(userCreationDTO.Password);
    }
    
    private static void ValidateUsername(string username)
    {
        if (username.Length < 3)
            throw new Exception("Username must be at least 3 characters!");

        if (username.Length > 15)
            throw new Exception("Username must be less than 16 characters!");
    }
    
    private static void ValidatePassword(string password)
    {
        string pattern = @"^(?=.*[A-Za-z])(?=.*\d).{8,}$";

        if (!Regex.IsMatch(password, pattern))
            throw new Exception("Password should consist of at least one uppercase and lowercase letter, and a digit, and must be at least 8 characters long.");

        if (password.Length > 20)
            throw new Exception("Password must be less than 20 characters!");
    }
}