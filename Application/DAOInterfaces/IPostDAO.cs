using Domain.DTOs.PostDTO;
using Domain.Model;

namespace Application.DAOInterfaces;

public interface IPostDAO
{
    
    Task<Post> CreateAsync(Post post);
    Task<Post?> GetByIdAsync(int id);

    Task<IEnumerable<Post>> GetAsync(SearchPostParametersDTO postParametersDto);

}