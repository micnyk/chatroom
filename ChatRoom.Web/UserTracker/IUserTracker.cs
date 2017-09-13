using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace ChatRoom.Web.UserTracker
{
    public interface IUserTracker
    {
        Task AddUser(HubConnectionContext connection, ChatUserDetails userDetails);
        Task RemoveUser(HubConnectionContext connection);
        Task<List<ChatUserDetails>> UsersOnline();
        Task<ChatUserDetails> GetUser(HubConnectionContext connection);
        Task<bool> UserExistsInRoom(HubConnectionContext connection, string roomId);
        Task<HubConnectionContext> GetUserContext(ChatUserDetails user);
    }
}