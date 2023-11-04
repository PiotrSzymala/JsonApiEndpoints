using SI_2.Models;

namespace SI_2.Services.JsonApiControllerService;

public interface IJsonApiControllerService
{
    Dictionary<char, int> CountCharacters(List<Post> posts);
    Task SavePosts(List<Post> posts);
    Task SaveUsers(List<User> users);
    Task SaveComments(List<Comment> comments);
}