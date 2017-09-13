using ChatRoom.Rooms.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoom.Web.Controllers
{
    [Route("/api/rooms")]
    public class RoomsController : BaseController
    {
        [HttpGet("")]
        public IActionResult GetRooms() => ProcessQuery(new GetRoomsQuery());

        [HttpGet("{id}")]
        public IActionResult GetRoom([FromQuery] GetRoomQuery query) => ProcessQuery(query);
    }
}