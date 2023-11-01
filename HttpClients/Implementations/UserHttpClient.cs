using Domain.DTOs.UserDTO;
using Domain.Model;
using HttpClient.ClientInterfaces;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace HttpClient.Implementations;

public class UserHttpClient:IUserService
{
    private readonly System.Net.Http.HttpClient client;
    
    public static string? Jwt { get; private set; } = "";

    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; } = null!;    

    public UserHttpClient(System.Net.Http.HttpClient client)
    {
        this.client = client;
    }

    public async Task LoginAsync(string username, string password)
    {
        UserLoginDTO userLoginDto = new()
        {
            Username = username,
            Password = password
        };

        string userAsJson = JsonSerializer.Serialize(userLoginDto);
        StringContent content = new(userAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync("users/login", content);
        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }

        string token = responseContent;
        Jwt = token;

        ClaimsPrincipal principal = CreateClaimsPrincipal();

        OnAuthStateChanged.Invoke(principal);
    }
    
    private static ClaimsPrincipal CreateClaimsPrincipal()
    {
        if (string.IsNullOrEmpty(Jwt))
        {
            return new ClaimsPrincipal();
        }

        IEnumerable<Claim> claims = ParseClaimsFromJwt(Jwt);
        
        ClaimsIdentity identity = new(claims, "jwt");

        ClaimsPrincipal principal = new(identity);
        return principal;
    }

    public Task LogoutAsync()
    {
        Jwt = null;
        ClaimsPrincipal principal = new();
        OnAuthStateChanged.Invoke(principal);
        return Task.CompletedTask;
    }

    public async Task CreateAsync(UserCreationDTO userToCreate)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/users", userToCreate);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
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

    public async Task<User> GetSingleAsync(SearchUserParametersDTO searchParameters)
    {

        HttpResponseMessage response = await client.GetAsync($"/users/{searchParameters.UsernameMatches}");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        Console.WriteLine(result);
        User user = JsonSerializer.Deserialize<User>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return user;
    }


    public Task<ClaimsPrincipal> GetAuthAsync()
    {
        ClaimsPrincipal principal = CreateClaimsPrincipal();
        return Task.FromResult(principal);
    }
    
    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        string payload = jwt.Split('.')[1];
        byte[] jsonBytes = ParseBase64WithoutPadding(payload);
        Dictionary<string, object>? keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        return keyValuePairs!.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!));
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
        }

        return Convert.FromBase64String(base64);
    }

}