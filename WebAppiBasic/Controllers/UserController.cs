using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppiBasic.Service;

namespace WebAppiBasic.Controllers
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

        //[AllowAnonymous]
        [HttpGet("BasicAuthenticate")]
        [Authorize]
        public async Task<IActionResult> GetWithBasicAuthorize()
        {

            var users = await _userService.GetAll();

            if (users == null)
            {
                return NotFound($"brak uzytkownikow!");
            }

            return Ok(users);
        }
    }
}
