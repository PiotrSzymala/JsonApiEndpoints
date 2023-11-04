using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using SI_2.Models;
using SI_2.Services.JsonApiControllerService;

namespace JsonApiEndpoints.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {

        private readonly RestClient _client = new("https://jsonplaceholder.typicode.com");
        private readonly IJsonApiControllerService _jsonApiControllerService;

        public PostController(IJsonApiControllerService jsonApiControllerService)
        {
            _jsonApiControllerService = jsonApiControllerService;
        }

        [HttpGet]
        [Route("/post")]
        public async Task<IActionResult> GetPost([FromQuery] string postId)
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
        [Route("/posts/sync")]
        public async Task<IActionResult> SyncPosts()
        {
            var request = new RestRequest($"/posts", Method.Get);
            var result = await _client.ExecuteAsync(request);

            var posts = JsonConvert.DeserializeObject<List<Post>>(result.Content);

            await _jsonApiControllerService.SavePosts(posts);

            return Ok();
        }
    }
}
