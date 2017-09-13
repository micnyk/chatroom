using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChatRoom.Users.Dtos;
using Microsoft.AspNetCore.SignalR;

namespace ChatRoom.Web.UserTracker
{
    public interface IUserTracker
    {
        event Action<ChatUserDetails[]> UsersJoined;
        event Action<ChatUserDetails[]> UsersLeft;

        Task AddUser(HubConnectionContext connection, ChatUserDetails userDetails);
        Task RemoveUser(HubConnectionContext connection);
        Task<IEnumerable<ChatUserDetails>> UsersOnline();
    }
}