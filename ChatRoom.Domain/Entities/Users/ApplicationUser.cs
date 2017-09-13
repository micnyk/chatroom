using Microsoft.AspNetCore.Identity;

namespace ChatRoom.Domain.Entities.Users
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsGuest { get; set; }
    }
}
