using ChatRoom.Infrastructure.CQS.Query;
using ChatRoom.Rooms.Dtos;

namespace ChatRoom.Rooms.Queries
{
    public class GetRoomQuery : IQuery<RoomDto>
    {
        public string Id { get; set; }
    }
}