using System;
using System.Linq;
using System.Threading.Tasks;
using ChatRoom.Web.Extensions;
using ChatRoom.Web.UserTracker;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ChatRoom.Web.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IUserTracker _userTracker;

        public ChatHub(IUserTracker userTracker)
        {
            _userTracker = userTracker;
        }

        public override async Task OnConnectedAsync()
        {
            var x = 5;
            await Clients.Client(Context.ConnectionId).InvokeAsync("SetUsersOnline"));

            await base.OnConnectedAsync();
        }

        public async Task ConnectRoom(string roomId)
        {
            await _userTracker.AddUser(Context.Connection, new ChatUserDetails
            {
                RoomId = roomId,
                UserId = Context.Connection.User.GetUserId()
            });

            var usersOnline = await _userTracker.UsersOnline();

            await Clients.All.InvokeAsync("RoomUsersOnline", usersOnline.Where(x => x.RoomId == roomId));
        }
    }
}