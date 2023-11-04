using JsonApiEndpoints.Models;

namespace JsonApiEndpoints.Services.JsonApiControllerService;

public interface IJsonApiControllerService
{
    Dictionary<char, int> CountCharacters(List<Post> posts);
    Task SavePosts(List<Post> posts);
    Task SaveUsers(List<User> users);
    Task SaveComments(List<Comment> comments);
}