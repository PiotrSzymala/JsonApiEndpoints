using SI_2.Models;

namespace SI_2.Services.JsonApiControllerService;

public interface IJsonApiControllerService
{
    Dictionary<char, int> CountCharacters(List<Post> posts);
}