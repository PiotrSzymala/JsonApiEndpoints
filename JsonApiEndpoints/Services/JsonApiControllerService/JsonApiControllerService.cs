using SI_2.Models;

namespace SI_2.Services.JsonApiControllerService;

public class JsonApiControllerService : IJsonApiControllerService
{
    public Dictionary<char, int> CountCharacters(List<Post> posts)
    {
        var charCount = posts
            .SelectMany(post => post.Body)
            .GroupBy(c => c)
            .ToDictionary(g => g.Key, g => g.Count());
    
        return charCount;
    }

}