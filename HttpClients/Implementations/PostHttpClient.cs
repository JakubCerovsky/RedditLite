using System.Net.Http.Json;
using System.Text.Json;
using Domain.DTOs.PostDTO;
using Domain.DTOs.UserDTO;
using Domain.Model;
using HttpClient.ClientInterfaces;

namespace HttpClient.Implementations;

public class PostHttpClient:IPostService
{
    private readonly System.Net.Http.HttpClient client;

    public PostHttpClient(System.Net.Http.HttpClient client)
    {
        this.client = client;
    }
    
    public async Task<Post> CreateAsync(PostCreationDTO postCreation)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/posts", postCreation);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        Post post = JsonSerializer.Deserialize<Post>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return post;
    }

    public async Task<IEnumerable<Post>> GetAsync()
    {
        HttpResponseMessage response = await client.GetAsync("/posts");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        Console.WriteLine(result);
        IEnumerable<Post> posts = JsonSerializer.Deserialize<IEnumerable<Post>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return posts;
    }
}