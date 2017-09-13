using System.Collections.Generic;
using ChatRoom.Infrastructure.CQS.Query;

namespace ChatRoom.Rooms.Dtos
{
    public class RoomsList : List<RoomDto>, IQueryResult
    {
    }
}