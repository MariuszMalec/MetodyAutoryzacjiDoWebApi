using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAppi.Models;
using WebAppi.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAppi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/<UserController>



        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = new List<Client> () 
            {
                new Client (){ FirstName = "John", LastName = "John" }
            };
            if (!users.Any())
                return NotFound($"Brak uzytkowników!");
            return Ok(users);
        }

        [HttpGet("{email}/{password}/{token}")]
        public async Task<IActionResult> Get([FromRoute] string email, string password, string token)
        {
            if (token != "123456")
            {
                return Unauthorized("Unauthorized 401!");
            }
            var users = new List<Client>()
            {
                new Client (){ FirstName = "Kate", LastName = "Kate" }
            };
            if (!users.Any())
                return NotFound($"Brak uzytkowników!");
            return Ok(users);
        }

        [AllowAnonymous]
        [HttpPost("authenticateAll")]
        public async Task<IActionResult> GetAllUsersWithAuthenticate([FromBody] AuthenticateModel model)
        {
            var user = await _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var users = await _userService.GetAll();

            if (users == null)
            {
                return NotFound($"brak uzytkownikow!");
            }

            return Ok(users);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
