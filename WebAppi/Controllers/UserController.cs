using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAppi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAppi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: api/<UserController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = new List<User> () 
            {
                new User (){ FirstName = "John", LastName = "Stokton" }
            };
            if (!users.Any())
                return NotFound($"Brak uzytkowników!");
            return Ok(users);
        }

        [HttpGet("{email}/{password}")]
        public async Task<IActionResult> Get([FromRoute] string email, string password)
        {
            if (email != "mariusz")
            {
                return Unauthorized("Unauthorized 401!");
            }
            var users = new List<User>()
            {
                new User (){ FirstName = "John", LastName = "Stokton" }
            };
            if (!users.Any())
                return NotFound($"Brak uzytkowników!");
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
