using System.Linq;
using ChatRoom.Domain.Entities.Rooms;
using ChatRoom.Domain.Repository;
using ChatRoom.Infrastructure.CQS.Query;
using ChatRoom.Rooms.Dtos;
using ChatRoom.Rooms.Queries;

namespace ChatRoom.Rooms.Handlers
{
    public class RoomsQueriesHandler :
        IQueryHandler<GetRoomsQuery, RoomsList>,
        IQueryHandler<GetRoomQuery, RoomDto>
    {
        private readonly IRepository<Room> _roomsRepository;

        public RoomsQueriesHandler(IRepository<Room> roomsRepository)
        {
            _roomsRepository = roomsRepository;
        }

        public RoomsList Handle(GetRoomsQuery query)
        {
            var q = _roomsRepository.Query();

            if (!string.IsNullOrEmpty(query.Id))
                q = q.Where(x => x.Id == query.Id);

            if (!string.IsNullOrEmpty(query.Name))
                q = q.Where(x => x.Name.Contains(query.Name));

            var result = new RoomsList();

            result.AddRange(q
                .Select(x => new RoomDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UsersOnline = 0,
                    UnreadMessages = 0
                })
                .ToList());

            return result;
        }

        public RoomDto Handle(GetRoomQuery query)
        {
            var room = _roomsRepository.Get(query.Id);

            if (room == null)
                return null;

            return new RoomDto
            {
                Id = room.Id,
                Name = room.Name
            };
        }
    }
}