using Newtonsoft.Json;

namespace SI_2.Models;

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