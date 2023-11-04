using Newtonsoft.Json;

namespace SI_2.Models;

public class Comment
{
    [JsonProperty("postId")]
    public int PostId { get; set; }

    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("body")]
    public string Body { get; set; }
    
    public Post Post { get; set; }
}