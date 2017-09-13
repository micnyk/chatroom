using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatRoom.Users.Dtos;
using ChatRoom.Web.Extensions;
using Microsoft.AspNetCore.SignalR;

namespace ChatRoom.Web.UserTracker
{
    public class InMemoryUserTracker : IUserTracker
    {
        private readonly ConcurrentDictionary<HubConnectionContext, ChatUserDetails> _usersOnline
            = new ConcurrentDictionary<HubConnectionContext, ChatUserDetails>();

        public event Action<ChatUserDetails[]> UsersJoined;
        public event Action<ChatUserDetails[]> UsersLeft;

        public Task<List<ChatUserDetails>> UsersOnline() => Task.FromResult(_usersOnline.Values.ToList());

        public Task<ChatUserDetails> GetUser(HubConnectionContext connection)
        {
            _usersOnline.TryGetValue(connection, out var user);
            return Task.FromResult(user);
        }

        public Task<bool> UserExistsInRoom(HubConnectionContext connection, string roomId)
        {
            return Task.FromResult(_usersOnline.Values.Any(x => x.UserId == connection.User.GetUserId()));
        }

        public Task AddUser(HubConnectionContext connection, ChatUserDetails userDetails)
        {
            _usersOnline.TryAdd(connection, userDetails);

            UsersJoined?.Invoke(new[] { userDetails });

            return Task.CompletedTask;
        }

        public Task RemoveUser(HubConnectionContext connection)
        {
            if (_usersOnline.TryRemove(connection, out var userDetails))
            {
                UsersLeft?.Invoke(new[] { userDetails });
            }

            return Task.CompletedTask;
        }
    }
}