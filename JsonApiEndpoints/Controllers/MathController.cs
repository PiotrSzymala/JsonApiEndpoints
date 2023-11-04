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

    /// <summary>
    /// Retrieves the sum of characters in the content of posts.
    /// </summary>
    /// <remarks>
    /// This method counts characters in posts retrieved from the JSON placeholder API.
    /// </remarks>
    /// <response code="200">Returns the total count of characters in posts.</response>
    /// <response code="400">If an error occurs in retrieving posts.</response>
    /// <returns>The count of characters in post contents.</returns>
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