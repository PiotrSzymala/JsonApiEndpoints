using RestSharp;

namespace SI_2.Client;

public class JsonClient : RestClient
{
    private const string apiUrl = "https://jsonplaceholder.typicode.com";
    private readonly RestClient _client = new(apiUrl);

}