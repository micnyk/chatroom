using Microsoft.AspNetCore.Identity;

namespace ChatRoom.Domain.Entities.User
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsGuest { get; set; }
    }
}
