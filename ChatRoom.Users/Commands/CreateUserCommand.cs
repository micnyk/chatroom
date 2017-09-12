using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ChatRoom.Infrastructure.CQS.Command;
using ChatRoom.Users.Dtos;

namespace ChatRoom.Users.Commands
{
    public class CreateUserCommand : ICommand<CreateUserResult>
    {
        [Required, MinLength(3), MaxLength(50)]
        [DisplayName("User name")]
        public string UserName { get; set; }

        public string Password { get; set; }

        public bool IsGuest { get; set; }
    }
}
