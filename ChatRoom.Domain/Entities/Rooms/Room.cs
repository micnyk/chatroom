using ChatRoom.Domain.Entities.Users;

namespace ChatRoom.Domain.Entities.Rooms
{
    public class Room : Entity
    {
        public string Name { get; set; }

        public string CreatedByUserId { get; set; }

        public virtual ApplicationUser CreatedByUser { get; set; }
    }
}