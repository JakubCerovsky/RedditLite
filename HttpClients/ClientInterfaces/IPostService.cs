using Domain.DTOs.PostDTO;
using Domain.Model;

namespace HttpClient.ClientInterfaces;

public interface IPostService
{
        Task<Post> CreateAsync(PostCreationDTO postCreation);
        Task<IEnumerable<Post>> GetAsync();
}