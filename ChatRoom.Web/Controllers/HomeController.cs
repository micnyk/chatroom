using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ChatRoom.Web.Controllers;

namespace ChatRoom_Web.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
