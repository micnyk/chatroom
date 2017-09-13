using System.Collections.Generic;
using ChatRoom.Infrastructure.CQS.Query;
using ChatRoom.Rooms.Dtos;

namespace ChatRoom.Rooms.Queries
{
    public class GetRoomsQuery : IQuery<RoomsList>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}