using Application.DAOInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs.PostDTO;
using Domain.Model;

namespace Application.Logic;

public class PostLogic:IPostLogic
{
    private readonly IPostDAO postDAO;
    private readonly IUserDAO userDAO;

    public PostLogic(IPostDAO postDAO, IUserDAO userDAO)
    {
        this.postDAO = postDAO;
        this.userDAO = userDAO;
    }

    public async Task<Post> CreateAsync(PostCreationDTO postToCreate)
    {
        ValidatePost(postToCreate);

        User? owner = await userDAO.GetByUsernameAsync(postToCreate.OwnerUsername);
        if (owner == null)
            throw new Exception($"User with Username - {postToCreate.OwnerUsername} - was not found.");

        Post post = new Post(owner.Username, postToCreate.Title, postToCreate.Body);
        Post created = await postDAO.CreateAsync(post);
        return created;
    }

    public async Task<IEnumerable<Post>> GetAsync(SearchPostParametersDTO postParametersDto)
    {
        return await postDAO.GetAsync(postParametersDto);
    }

    public async Task<Post> GetSingleByIdAsync(int id)
    {
        return await postDAO.GetByIdAsync(id);
    }

    private static void ValidatePost(PostCreationDTO postToValidate)
    {
        if (string.IsNullOrEmpty(postToValidate.Title)) 
            throw new Exception("Title cannot be empty.");
        
        if (string.IsNullOrEmpty(postToValidate.Body)) 
            throw new Exception("Body cannot be empty.");
        
        if (string.IsNullOrEmpty(postToValidate.OwnerUsername))
            throw new Exception($"Username has to be set.");
    }
}