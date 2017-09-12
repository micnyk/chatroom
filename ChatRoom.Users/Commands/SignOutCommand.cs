using ChatRoom.Infrastructure.CQS.Command;
using ChatRoom.Users.Dtos;

namespace ChatRoom.Users.Commands
{
    public class SignOutCommand : ICommand<SignOutResult>
    {
    }
}