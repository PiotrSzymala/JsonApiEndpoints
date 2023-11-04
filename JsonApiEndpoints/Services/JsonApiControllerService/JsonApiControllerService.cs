using JsonApiEndpoints.Entities;
using JsonApiEndpoints.Models;
using Microsoft.EntityFrameworkCore;

namespace JsonApiEndpoints.Services.JsonApiControllerService;

public class JsonApiControllerService : IJsonApiControllerService
{
    private readonly ApplicationDbContext _context;

    public JsonApiControllerService(ApplicationDbContext context)
    {
        _context = context;
    }

    public Dictionary<char, int> CountCharacters(List<Post> posts)
    {
        var charCount = posts
            .SelectMany(post => post.Body)
            .GroupBy(c => c)
            .ToDictionary(g => g.Key, g => g.Count());
    
        return charCount;
    }

    public async Task SavePosts(List<Post> posts)
    {
        var existingPosts = await _context.Posts
            .Where(p => posts.Select(np => np.Title).Contains(p.Title) &&
                        posts.Select(np => np.UserId).Contains(p.UserId))
            .ToListAsync();

        var postsToAdd = posts.Where(newPost =>
                !existingPosts.Any(existingPost =>
                    existingPost.Title == newPost.Title && existingPost.UserId == newPost.UserId))
            .ToList();

        if (postsToAdd.Any())
        {
            await _context.Posts.AddRangeAsync(postsToAdd);
            await _context.SaveChangesAsync();
        }
    }


    public async Task SaveComments(List<Comment> comments)
    {
        var existingComments = await _context.Comments
            .Where(c => comments.Select(nc => nc.Body).Contains(c.Body) &&
                        comments.Select(nc => nc.PostId).Contains(c.PostId))
            .ToListAsync();

        var commentsToAdd = comments.Where(newComment =>
                !existingComments.Any(existingComment =>
                    existingComment.Body == newComment.Body && existingComment.PostId == newComment.PostId))
            .ToList();

        if (commentsToAdd.Any())
        {
            await _context.Comments.AddRangeAsync(commentsToAdd);
            await _context.SaveChangesAsync();
        }
    }

    public async Task SaveUsers(List<User> users)
    {
        var existingUsers = await _context.Users
            .Where(u => users.Select(nu => nu.Email).Contains(u.Email))
            .ToListAsync();

        var usersToAdd = users.Where(newUser =>
                !existingUsers.Any(existingUser => existingUser.Email == newUser.Email))
            .ToList();

        if (usersToAdd.Any())
        {
            await _context.Users.AddRangeAsync(usersToAdd);
            await _context.SaveChangesAsync();
        }
    }

}