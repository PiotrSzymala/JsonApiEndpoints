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
    public class CommentController : ControllerBase
    {
        private readonly RestClient _client = new("https://jsonplaceholder.typicode.com");
        private readonly IJsonApiControllerService _jsonApiControllerService;

        public CommentController(IJsonApiControllerService jsonApiControllerService)
        {
            _jsonApiControllerService = jsonApiControllerService;
        }

        [HttpGet]
        [Route("/post/comments")]
        public async Task<IActionResult> GetCommentsFromPost([FromQuery] string postId)
        {
            var request = new RestRequest($"/comments?postId={postId}", Method.Get);
            var result = await _client.ExecuteAsync(request);

            return Ok(result.Content);
        }

        [HttpGet]
        [Route("/comments")]
        public async Task<IActionResult> GetComments()
        {
            var request = new RestRequest($"/comments", Method.Get);
            var result = await _client.ExecuteAsync(request);

            return Ok(result.Content);
        }

        [HttpGet]
        [Route("/comments/sync")]
        public async Task<IActionResult> SyncComments()
        {
            var request = new RestRequest($"/comments", Method.Get);
            var result = await _client.ExecuteAsync(request);

            var comments = JsonConvert.DeserializeObject<List<Comment>>(result.Content);

            await _jsonApiControllerService.SaveComments(comments);

            return Ok();
        }
    }
}
