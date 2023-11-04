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
    public class CommentController : ControllerBase
    {
        private readonly RestClient _client = new("https://jsonplaceholder.typicode.com");
        private readonly IJsonApiControllerService _jsonApiControllerService;

        public CommentController(IJsonApiControllerService jsonApiControllerService)
        {
            _jsonApiControllerService = jsonApiControllerService;
        }


        /// <summary>
        /// Retrieves comments for a specific post by the post identifier.
        /// </summary>
        /// <param name="postId">The unique identifier of the post for which comments are to be retrieved.</param>
        /// <returns>An ActionResult of type string containing the comments data.</returns>
        /// <response code="200">Returns the requested comments data.</response>
        /// <response code="400">If the postId is null, empty, or incorrect, leading to a bad request.</response>
        [HttpGet]
        [Route("/post/comments")]
        public async Task<IActionResult> GetCommentsFromPost([FromQuery, Required] string postId)
        {
            var request = new RestRequest($"/comments?postId={postId}", Method.Get);
            var result = await _client.ExecuteAsync(request);

            if (!string.IsNullOrEmpty(result.Content))
                return BadRequest("Wrong id");

            return Ok(result.Content);
        }

        /// <summary>
        /// Retrieves all comments.
        /// </summary>
        /// <returns>An ActionResult of type string containing all comments data.</returns>
        /// <response code="200">Returns all comments data.</response>
        [HttpGet]
        [Route("/comments")]
        public async Task<IActionResult> GetComments()
        {
            var request = new RestRequest($"/comments", Method.Get);
            var result = await _client.ExecuteAsync(request);

            return Ok(result.Content);
        }

        /// <summary>
        /// Synchronizes comments from an external service into the local system.
        /// </summary>
        /// <remarks>
        /// This operation fetches comments from the external service and updates the local data store.
        /// Existing comments with matching criteria will not be duplicated.
        /// </remarks>
        /// <returns>An ActionResult indicating the result of the synchronization operation.</returns>
        /// <response code="200">If the synchronization is successful.</response>
        /// <response code="500">If an error occurs during synchronization.</response>
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
