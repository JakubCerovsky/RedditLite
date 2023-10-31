using Domain.DTOs.UserDTO;
using Domain.Model;
using HttpClient.ClientInterfaces;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace HttpClient.Implementations;

public class UserHttpClient:IUserService
{
    private readonly System.Net.Http.HttpClient client;

    public UserHttpClient(System.Net.Http.HttpClient client)
    {
        this.client = client;
    }
    public async Task<User> CreateAsync(UserCreationDTO userToCreate)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/users", userToCreate);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        User user = JsonSerializer.Deserialize<User>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return user;
    }

    public async Task<IEnumerable<User>> GetAsync(SearchUserParametersDTO searchParameters)
    {
        string uri = "/users";
        if (!string.IsNullOrEmpty(searchParameters.UsernameContains))
        {
            uri += $"?username={searchParameters.UsernameContains}";
        }
        HttpResponseMessage response = await client.GetAsync(uri);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        Console.WriteLine(result);
        IEnumerable<User> users = JsonSerializer.Deserialize<IEnumerable<User>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return users;
    }
}