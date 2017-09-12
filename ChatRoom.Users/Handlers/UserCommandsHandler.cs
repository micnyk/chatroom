using System.Linq;
using System.Threading.Tasks;
using ChatRoom.Domain.Entities.User;
using ChatRoom.Infrastructure.CQS.Command;
using ChatRoom.Users.Commands;
using ChatRoom.Users.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SignInResult = ChatRoom.Users.Dtos.SignInResult;

namespace ChatRoom.Users.Handlers
{
    public class UserCommandsHandler : 
        ICommandHandler<RegisterCommand, RegisterResult>, 
        ICommandHandler<SignInCommand, SignInResult>,
        ICommandHandler<SignOutCommand, SignOutResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserCommandsHandler(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _contextAccessor = contextAccessor;
        }

        public RegisterResult Handle(RegisterCommand command)
        {
            var user = new ApplicationUser
            {
                IsGuest = command.IsGuest,
                UserName = command.UserName
            };

            var createUserResult = _userManager.CreateAsync(user).Result;

            if (!createUserResult.Succeeded)
                return new RegisterResult
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
                    return new RegisterResult
                    {
                        Success = false,
                        Errors = addPasswordResult.Errors.Select(x => x.Description).ToArray()
                    };
                }
            }

            Task.Run(() => _signInManager.SignInAsync(user, true)).Wait();

            return new RegisterResult
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

        public SignInResult Handle(SignInCommand command)
        {
            var user = _userManager.FindByNameAsync(command.UserName).Result;

            if (user == null)
                return new SignInResult {Success = false};

            var result = _signInManager.PasswordSignInAsync(user, command.Password, true, false).Result;

            return new SignInResult
            {
                Success = result.Succeeded,
                User = new UserDto
                {
                    Id = user.Id,
                    IsGuest = user.IsGuest,
                    UserName = user.UserName
                }
            };
        }

        public SignOutResult Handle(SignOutCommand command)
        {
            Task.Run(() => _signInManager.SignOutAsync()).Wait();

            var user = _userManager.GetUserAsync(_contextAccessor.HttpContext.User).Result;

            if (user != null && user.IsGuest)
            {
                var deleteResult = _userManager.DeleteAsync(user).Result;
            }

            return new SignOutResult();
        }
    }
}
