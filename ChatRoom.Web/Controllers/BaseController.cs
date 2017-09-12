using System;
using ChatRoom.Infrastructure;
using ChatRoom.Infrastructure.CQS.Command;
using ChatRoom.Infrastructure.CQS.Query;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoom.Web.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult ProcessCommand<TResult>(ICommand<TResult> command) where TResult : ICommandResult
        {
            var bus = GetBus();

            var result = bus.Process(command, ModelState);

            return Json(result);
        }

        public IActionResult ProcessQuery<TResult>(IQuery<TResult> query) where TResult : IQueryResult
        {
            var bus = GetBus();

            var result = bus.Process(query);

            return Json(result);
        }

        private IBus GetBus()
        {
            if (!(HttpContext.RequestServices.GetService(typeof(IBus)) is IBus bus))
                throw new InvalidOperationException();

            return bus;
        }
    }
}
