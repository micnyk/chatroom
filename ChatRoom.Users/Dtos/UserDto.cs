using ChatRoom.Infrastructure.CQS.Command;

namespace ChatRoom.Users.Dtos
{
    public class UserDto : ICommandResult
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public bool IsGuest { get; set; }
    }
}
