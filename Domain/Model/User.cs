﻿namespace Domain.Model;

public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public List<Post> Posts { get; set; }

    public User()
    {
        Posts = new List<Post>();
    }
}