using Newtonsoft.Json;

namespace JsonApiEndpoints.Models;

public class Address
{
    public int Id { get; set; }
    public int UserId { get; set; }
    [JsonProperty("street")]
    public string Street { get; set; }

    [JsonProperty("suite")]
    public string Suite { get; set; }

    [JsonProperty("city")]
    public string City { get; set; }

    [JsonProperty("zipcode")]
    public string Zipcode { get; set; }

    [JsonProperty("geo")]
    public Geo Geo { get; set; }

    public User User { get; set; }
}