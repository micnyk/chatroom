using ChatRoom.Users.Commands;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoom.Web.Controllers
{
    [Route("/api/user")]
    public class UserController : BaseController
    {
        [HttpPost("")]
        public IActionResult CreateUser(CreateUserCommand command) => ProcessCommand(command);
    }
}