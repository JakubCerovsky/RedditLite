using Domain.DTOs.PostDTO;
using Domain.Model;

namespace Application.LogicInterfaces;

public interface IPostLogic
{
    Task<Post> CreateAsync(PostCreationDTO postToCreate);
    Task<IEnumerable<Post>> GetAsync();

}