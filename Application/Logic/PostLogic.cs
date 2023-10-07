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
        User? owner = await userDAO.GetByIdAsync(postToCreate.OwnerId);
        if (owner == null)
            throw new Exception($"User with ID - {postToCreate.OwnerId} - was not found.");

        ValidatePost(postToCreate);
        Post post = new Post(owner, postToCreate.Title, postToCreate.Body);
        Post created = await postDAO.CreateAsync(post);
        return created;
    }

    private static void ValidatePost(PostCreationDTO postToValidate)
    {
        if (string.IsNullOrEmpty(postToValidate.Title)) 
            throw new Exception("Title cannot be empty.");
        
        if (string.IsNullOrEmpty(postToValidate.Body)) 
            throw new Exception("Body cannot be empty.");
    }
}