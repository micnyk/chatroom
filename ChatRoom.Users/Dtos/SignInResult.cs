using ChatRoom.Infrastructure.CQS.Command;

namespace ChatRoom.Users.Dtos
{
    public class SignInResult : ICommandResult
    {
        public bool Success { get; set; }

        public UserDto User { get; set; }
    }
}