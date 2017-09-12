using ChatRoom.Infrastructure.CQS.Command;
using ChatRoom.Users.Dtos;

namespace ChatRoom.Users.Commands
{
    public class SignInCommand : ICommand<SignInResult>
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}