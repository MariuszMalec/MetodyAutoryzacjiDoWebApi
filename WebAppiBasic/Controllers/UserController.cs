using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppiBasic.Service;

namespace WebAppiBasic.Controllers
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
