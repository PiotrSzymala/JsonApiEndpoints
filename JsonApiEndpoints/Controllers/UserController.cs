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
    public class UserController : ControllerBase
    {
        private readonly RestClient _client = new("https://jsonplaceholder.typicode.com");
        private readonly IJsonApiControllerService _jsonApiControllerService;

        public UserController(IJsonApiControllerService jsonApiControllerService)
        {
            _jsonApiControllerService = jsonApiControllerService;
        }

        [HttpGet]
        [Route("/user")]
        public async Task<IActionResult> GetUser([FromQuery] string userId)
        {
            var request = new RestRequest($"/users/{userId}", Method.Get);
            var result = await _client.ExecuteAsync(request);

            return Ok(result.Content);
        }

        [HttpGet]
        [Route("/users")]
        public async Task<IActionResult> GetUserS()
        {
            var request = new RestRequest($"/users", Method.Get);
            var result = await _client.ExecuteAsync(request);

            return Ok(result.Content);
        }

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
