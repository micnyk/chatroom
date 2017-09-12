using ChatRoom.Users.Commands;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoom.Web.Controllers
{
    [Route("/api/user")]
    public class UserController : BaseController
    {
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterCommand command) => ProcessCommand(command);

        [HttpPost("signIn")]
        public IActionResult SignIn([FromBody] SignInCommand command) => ProcessCommand(command);

        [HttpPost("signOut")]
        public IActionResult SignOut([FromBody] SignOutCommand command) => ProcessCommand(command);
    }
}