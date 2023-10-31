using Application.DAOInterfaces;
using Domain.Model;

namespace FileData.DAO;

public class PostFileDAO:IPostDAO
{
    
    private readonly FileContext context;

    public PostFileDAO(FileContext context)
    {
        this.context = context;
    }

    public Task<Post> CreateAsync(Post post)
    {
        int postId = 1;
        if (context.Posts.Any())
        {
            postId = context.Posts.Max(p => p.Id);
            postId++;
        }

        post.Id = postId;

        context.Posts.Add(post);
        context.Users
            .FirstOrDefault(user => post.OwnerUsername.Equals(user.Username, StringComparison.OrdinalIgnoreCase)).Posts
            .Add(post);
        context.SaveChanges();



        return Task.FromResult(post);
    }

    public Task<IEnumerable<Post>> GetAsync()
    {
        return Task<IEnumerable<Post>>.FromResult(context.Posts.AsEnumerable());
    }
}