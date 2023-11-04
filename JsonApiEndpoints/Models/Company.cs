using Newtonsoft.Json;

namespace JsonApiEndpoints.Models;

public class Company
{
    public int Id { get; set; }
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("catchPhrase")]
    public string CatchPhrase { get; set; }

    [JsonProperty("bs")]
    public string Bs { get; set; }
}