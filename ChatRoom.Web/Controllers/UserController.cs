using ChatRoom.Users.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoom.Web.Controllers
{
    [Authorize]
    [Route("/api/user")]
    public class UserController : BaseController
    {
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterCommand command) => ProcessCommand(command);

        [AllowAnonymous]
        [HttpPost("signIn")]
        public IActionResult SignIn([FromBody] SignInCommand command) => ProcessCommand(command);

        [HttpPost("signOut")]
        public IActionResult SignOut([FromBody] SignOutCommand command) => ProcessCommand(command);
    }
}