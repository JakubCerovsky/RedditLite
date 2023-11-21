using Application.LogicInterfaces;
using Domain.DTOs.PostDTO;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController:ControllerBase
{
    private readonly IPostLogic postLogic;

    public PostsController(IPostLogic postLogic)
    {
        this.postLogic = postLogic;
    }
    [HttpPost]
    public async Task<ActionResult<Post>> CreateAsync(PostCreationDTO dto)
    {
        try
        {
            Post post = await postLogic.CreateAsync(dto);
            return Created($"/posts/{post.Id}", post);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetAsync([FromQuery] string? ownerUsername)
    {
        try
        {
            SearchPostParametersDTO parameters = new(ownerUsername);
            IEnumerable<Post> posts = await postLogic.GetAsync(parameters);
            return Ok(posts);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("{postId}", Name = "GetSinglePostAsync")]
    public async Task<ActionResult<Post>> GetSingleAsync([FromRoute] int postId)
    {
        try
        {
            Post post = await postLogic.GetSingleByIdAsync(postId);
            return Ok(post);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}