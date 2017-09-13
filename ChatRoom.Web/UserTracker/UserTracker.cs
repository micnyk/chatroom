using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatRoom.Users.Dtos;
using Microsoft.AspNetCore.SignalR;

namespace ChatRoom.Web.UserTracker
{
    public class InMemoryUserTracker : IUserTracker
    {
        private readonly ConcurrentDictionary<HubConnectionContext, ChatUserDetails> _usersOnline
            = new ConcurrentDictionary<HubConnectionContext, ChatUserDetails>();

        public event Action<ChatUserDetails[]> UsersJoined;
        public event Action<ChatUserDetails[]> UsersLeft;

        public Task<IEnumerable<ChatUserDetails>> UsersOnline() => Task.FromResult(_usersOnline.Values.AsEnumerable());

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