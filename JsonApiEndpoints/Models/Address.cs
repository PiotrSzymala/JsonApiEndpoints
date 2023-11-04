using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SI_2.Models;

public class Address
{
    [Key]
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