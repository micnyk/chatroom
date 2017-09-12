using ChatRoom.Infrastructure.CQS.Command;

namespace ChatRoom.Users.Dtos
{
    public class RegisterResult : ICommandResult
    {
        public bool Success { get; set; }

        public UserDto User { get; set; }

        public string[] Errors { get; set; }
    }
}