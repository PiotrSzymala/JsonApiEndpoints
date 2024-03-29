using Newtonsoft.Json;

namespace JsonApiEndpoints.Models;

public class Post
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("userId")]
    public int UserId { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("body")]
    public string Body { get; set; }
    
    public User User { get; set; }
    public ICollection<Comment> Comments { get; set; }
}