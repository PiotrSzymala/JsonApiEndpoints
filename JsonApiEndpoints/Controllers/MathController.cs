using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using SI_2.Models;
using SI_2.Services.JsonApiControllerService;

namespace JsonApiEndpoints.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MathController : ControllerBase
{
    private readonly RestClient _client = new("https://jsonplaceholder.typicode.com");
    private readonly IJsonApiControllerService _jsonApiControllerService;

    public MathController(IJsonApiControllerService jsonApiControllerService)
    {
        _jsonApiControllerService = jsonApiControllerService;
    }

    [HttpGet]
    [Route("/calculate")]
    public async Task<IActionResult> GetNumbers()
    {
        var request = new RestRequest($"/posts", Method.Get);
        var result = await _client.ExecuteAsync(request);

        var posts = JsonConvert.DeserializeObject<List<Post>>(result.Content);

        var countedCharacters = _jsonApiControllerService.CountCharacters(posts);

        return Ok(countedCharacters);
    }
}