using Microsoft.AspNetCore.Identity;

namespace ChatRoom.Domain.Entities.User
{
    public class ApplicationUser : IdentityUser<string>
    {
        public bool IsGuest { get; set; }
    }
}
