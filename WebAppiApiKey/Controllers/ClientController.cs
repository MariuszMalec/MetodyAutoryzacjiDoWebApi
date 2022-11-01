using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppi.Models;
using WebAppiApiKey.Authentication.ApiKey;
using WebAppiApiKey.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAppiApiKey.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IUserService _clientService;

        public ClientController(IUserService userService)
        {
            _clientService = userService;
        }

        [HttpGet("ApiKey")]
        //[Authorize(AuthenticationSchemes = ApiKeyAuthenticationOptions.AuthenticationScheme)]Authorize
        [Authorize]
        public async Task<IActionResult> GetWithApiKey()
        {
            var users = new List<Client>()
            {
                new Client (){ FirstName = "John", LastName = "John" }
            };
            if (!users.Any())
                return NotFound($"Brak uzytkowników!");
            return Ok(users);
        }

        // GET: api/<ClientController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ClientController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ClientController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ClientController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ClientController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
