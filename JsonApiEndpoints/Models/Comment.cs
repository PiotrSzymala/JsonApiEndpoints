using Newtonsoft.Json;

namespace JsonApiEndpoints.Models;

public class Comment
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("postId")]
    public int PostId { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("body")]
    public string Body { get; set; }
    
    public Post Post { get; set; }
}