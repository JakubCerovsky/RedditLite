using Application.DAOInterfaces;
using Domain.DTOs.PostDTO;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class PostDAO:IPostDAO
{
    private readonly RedditContext context;

    public PostDAO(RedditContext context)
    {
        this.context = context;
    }
    public async Task<Post> CreateAsync(Post post)
    {
        EntityEntry<Post> added = await context.Posts.AddAsync(post);
        await context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<Post?> GetByIdAsync(int id)
    {
        Post? found = await context.Posts
            .Include(post => post.Owner)
            .SingleOrDefaultAsync(post => post.Id == id);
        return found;
    }

    public async Task<IEnumerable<Post>> GetAsync(SearchPostParametersDTO postParametersDto)
    {
        IQueryable<Post> query = context.Posts.Include(post => post.Owner).AsQueryable();
        
        if (!string.IsNullOrEmpty(postParametersDto.OwnerUsername))
        {
            // we know username is unique, so just fetch the first
            query = query.Where(post =>
                post.OwnerUsername.ToLower().Equals(postParametersDto.OwnerUsername.ToLower()));
        }
        
        List<Post> result = await query.ToListAsync();
        return result;
    }
}