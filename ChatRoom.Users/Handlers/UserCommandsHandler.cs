using System.Linq;
using ChatRoom.Domain.Entities.User;
using ChatRoom.Infrastructure;
using ChatRoom.Infrastructure.CQS.Command;
using ChatRoom.Users.Commands;
using ChatRoom.Users.Dtos;
using Microsoft.AspNetCore.Identity;

namespace ChatRoom.Users.Handlers
{
    public class UserCommandsHandler
        : ICommandHandler<CreateUserCommand, CreateUserResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserCommandsHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public CreateUserResult Handle(CreateUserCommand command)
        {
            var user = new ApplicationUser
            {
                IsGuest = command.IsGuest,
                UserName = command.UserName
            };

            var createUserResult = _userManager.CreateAsync(user).Result;

            if (!createUserResult.Succeeded)
                return new CreateUserResult
                {
                    Success = false,
                    Errors = createUserResult.Errors.Select(x => x.Description).ToArray()
                };

            if (!command.IsGuest)
            {
                var addPasswordResult = _userManager.AddPasswordAsync(user, command.Password).Result;

                if (!addPasswordResult.Succeeded)
                {
                    var deleteUserResult = _userManager.DeleteAsync(user).Result;
                    return new CreateUserResult
                    {
                        Success = false,
                        Errors = addPasswordResult.Errors.Select(x => x.Description).ToArray()
                    };
                }
            }

            return new CreateUserResult
            {
                Success = true,
                User = new UserDto
                {
                    Id = user.Id,
                    IsGuest = user.IsGuest,
                    UserName = user.UserName
                }
            };
        }
    }
}
