using ChatRoom.Domain.Entities.User;
using ChatRoom.Infrastructure.CQS.Command;
using ChatRoom.Users.Commands;
using ChatRoom.Users.Dtos;
using Microsoft.AspNetCore.Identity;

namespace ChatRoom.Users.Handlers
{
    public class UserCommandsHandler 
        : ICommandHandler<CreateUserCommand, UserDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserCommandsHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public UserDto Handle(CreateUserCommand command)
        {
            var user = new ApplicationUser
            {
                IsGuest = command.IsGuest,
                UserName = command.UserName
            };

            throw new System.NotImplementedException();
        }
    }
}
