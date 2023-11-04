using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using SI_2.Client;
using SI_2.Models;
using SI_2.Services.JsonApiControllerService;

namespace SI_2.Controllers;

[ApiController]
[Route("/json")]
public class JsonApiController : ControllerBase
{
    private readonly RestClient _client = new("https://jsonplaceholder.typicode.com");
    private readonly IJsonApiControllerService _jsonApiControllerService;

    public JsonApiController(IJsonApiControllerService jsonApiControllerService)
    {
        _jsonApiControllerService = jsonApiControllerService;
    }

    [HttpGet]
    [Route("/post")]
    public async Task<IActionResult> GetPost([FromQuery]string postId)
    {
        var request = new RestRequest($"/posts/{postId}", Method.Get);
        var result = await _client.ExecuteAsync(request);
        
        return Ok(result.Content);
    }
    
    [HttpGet]
    [Route("/posts")]
    public async Task<IActionResult> GetPosts()
    {
        var request = new RestRequest($"/posts", Method.Get);
        var result = await _client.ExecuteAsync(request);
       
        return Ok(result.Content);
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