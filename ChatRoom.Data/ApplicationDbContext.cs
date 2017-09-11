using ChatRoom.Domain.Entities.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ChatRoom.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
    }
}
