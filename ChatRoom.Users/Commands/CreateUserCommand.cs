using ChatRoom.Infrastructure.CQS.Command;
using ChatRoom.Users.Dtos;

namespace ChatRoom.Users.Commands
{
    public class CreateUserCommand : ICommand<UserDto>
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public bool IsGuest { get; set; }
    }
}
