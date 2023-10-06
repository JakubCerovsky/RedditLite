using System.Text.RegularExpressions;
using Application.DAOInterfaces;
using Application.LogicInterfaces;
using Domain.DTO;
using Domain.Model;

namespace Application.Logic;

public class UserLogic:IUserLogic
{
    private readonly IUserDAO userDAO;

    public UserLogic(IUserDAO userDAO)
    {
        this.userDAO = userDAO;
    }
    
    public async Task<User> CreateAsync(UserCreationDTO userToCreate)
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
    
        User created = await userDAO.CreateAsync(toCreate);
    
        return created;
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