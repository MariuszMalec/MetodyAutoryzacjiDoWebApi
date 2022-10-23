using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppi.Models;
using WebAppiApiKey.Authentication.ApiKey;
using WebAppiApiKey.Service;

namespace WebAppiApiKey.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("ApiKey")]
        [Authorize(AuthenticationSchemes = ApiKeyAuthenticationOptions.AuthenticationScheme)]
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
    }
}
