using ChatRoom.Infrastructure.CQS.Query;

namespace ChatRoom.Rooms.Dtos
{
    public class RoomDto : IQueryResult
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int UsersOnline { get; set; }
    }
}