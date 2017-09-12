using System;
using ChatRoom.Infrastructure;
using ChatRoom.Infrastructure.CQS.Command;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoom.Web.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult ProcessCommand<TResult>(ICommand<TResult> command) where TResult: ICommandResult
        {
            if(!(HttpContext.RequestServices.GetService(typeof(IRequestProcessor)) is IRequestProcessor requestProcessor))
                throw new InvalidOperationException();

            var handler = HttpContext.RequestServices
                .GetService(typeof(ICommandHandler<,>)
                .MakeGenericType(command.GetType(), typeof(TResult)));

            var result = requestProcessor.Process(command, handler, ModelState);

            return Json(result);
        }
    }
}
