using Newtonsoft.Json;

namespace JsonApiEndpoints.Models;

public class Geo
{
    public int Id { get; set; }
    public int AddressId { get; set; }
    [JsonProperty("lat")]
    public string Lat { get; set; }

    [JsonProperty("lng")]
    public string Lng { get; set; }

}