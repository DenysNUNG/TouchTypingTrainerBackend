using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TouchTypingTrainerBackend.Services;

namespace TouchTypingTrainerBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("get-email")]
        public IActionResult GetUserEmail()
        {
            return Ok(_userService.GetUserEmail());
        }
    }
}
