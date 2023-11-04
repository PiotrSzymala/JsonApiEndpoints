using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using SI_2.Models;
using SI_2.Services.JsonApiControllerService;
using System.ComponentModel.DataAnnotations;

namespace JsonApiEndpoints.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly RestClient _client = new("https://jsonplaceholder.typicode.com");
        private readonly IJsonApiControllerService _jsonApiControllerService;

        public UserController(IJsonApiControllerService jsonApiControllerService)
        {
            _jsonApiControllerService = jsonApiControllerService;
        }

        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to be retrieved.</param>
        /// <returns>An ActionResult of type string containing the user data.</returns>
        /// <response code="200">Returns the requested user data.</response>
        /// <response code="400">If the userId is null or empty.</response>
        [HttpGet]
        [Route("/user")]
        public async Task<IActionResult> GetUser([FromQuery, Required] string userId)
        {
            var request = new RestRequest($"/users/{userId}", Method.Get);
            var result = await _client.ExecuteAsync(request);

            if (!string.IsNullOrEmpty(result.Content))
                return BadRequest("Wrong id");
            

            return Ok(result.Content);
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>An ActionResult of type string containing all users data.</returns>
        /// <response code="200">Returns all users data.</response>
        [HttpGet]
        [Route("/users")]
        public async Task<IActionResult> GetUserS()
        {
            var request = new RestRequest($"/users", Method.Get);
            var result = await _client.ExecuteAsync(request);

            return Ok(result.Content);
        }

        /// <summary>
        /// Synchronizes users from an external service into the local system.
        /// </summary>
        /// <remarks>
        /// This operation will fetch users from the external service and update the local data store.
        /// Any existing users with the same ID will be updated.
        /// </remarks>
        /// <returns>An ActionResult indicating the result of the synchronization operation.</returns>
        /// <response code="200">If the synchronization is successful.</response>
        /// <response code="500">If an error occurs during synchronization.</response>
        [HttpGet]
        [Route("/users/sync")]
        public async Task<IActionResult> SyncUsers()
        {
            var request = new RestRequest($"/users", Method.Get);
            var result = await _client.ExecuteAsync(request);

            var users = JsonConvert.DeserializeObject<List<User>>(result.Content);

            await _jsonApiControllerService.SaveUsers(users);

            return Ok();
        }

    }
}
