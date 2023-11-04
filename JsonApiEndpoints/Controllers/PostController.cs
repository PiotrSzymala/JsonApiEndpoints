using System.ComponentModel.DataAnnotations;
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

        /// <summary>
        /// Retrieves a post by its unique identifier.
        /// </summary>
        /// <param name="postId">The unique identifier of the post to be retrieved.</param>
        /// <returns>An ActionResult of type string containing the post data.</returns>
        /// <response code="200">Returns the requested post data.</response>
        /// <response code="400">If the postId is null, empty, or incorrect.</response>
        [HttpGet]
        [Route("/post")]
        public async Task<IActionResult> GetPost([FromQuery, Required] string postId)
        {
            var request = new RestRequest($"/posts/{postId}", Method.Get);
            var result = await _client.ExecuteAsync(request);

            if (!string.IsNullOrEmpty(result.Content))
                return BadRequest("Wrong id");

            return Ok(result.Content);
        }

        /// <summary>
        /// Retrieves all posts.
        /// </summary>
        /// <returns>An ActionResult of type string containing all post data.</returns>
        /// <response code="200">Returns all posts data.</response>
        [HttpGet]
        [Route("/posts")]
        public async Task<IActionResult> GetPosts()
        {
            var request = new RestRequest($"/posts", Method.Get);
            var result = await _client.ExecuteAsync(request);

            return Ok(result.Content);
        }

        /// <summary>
        /// Synchronizes posts from an external service into the local system.
        /// </summary>
        /// <remarks>
        /// This operation fetches posts from the external service and updates the local data store.
        /// Any existing posts with the same ID will be updated.
        /// </remarks>
        /// <returns>An ActionResult indicating the result of the synchronization operation.</returns>
        /// <response code="200">If the synchronization is successful.</response>
        /// <response code="500">If an error occurs during synchronization.</response>
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
