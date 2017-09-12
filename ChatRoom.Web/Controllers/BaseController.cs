using ChatRoom.Infrastructure;
using ChatRoom.Users.Commands;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoom.Web.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            var requestProcessor = AppServiceProvider.Provider.GetService(typeof(IRequestProcessor)) as IRequestProcessor;

            var x = requestProcessor.Process(new CreateUserCommand { UserName = "ddd" });
        }
    }
}
