using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatRoom.Infrastructure;
using ChatRoom.Rooms.Queries;
using ChatRoom.Web.Extensions;
using ChatRoom.Web.UserTracker;
using Microsoft.AspNetCore.SignalR;

namespace ChatRoom.Web.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IUserTracker _userTracker;
        private readonly IBus _bus;

        public ChatHub(IUserTracker userTracker, IBus bus)
        {
            _userTracker = userTracker;
            _bus = bus;
        }

        public override async Task OnConnectedAsync()
        {
            var usersOnline = await _userTracker.UsersOnline();
            await SendRoomsOnlineUsers(usersOnline);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var user = await _userTracker.GetUser(Context.Connection);

            await _userTracker.RemoveUser(Context.Connection);

            var usersOnline = await _userTracker.UsersOnline();

            await SendRoomsOnlineUsers(usersOnline);
            await SendRoomUsers(user.RoomId, usersOnline);

            await base.OnDisconnectedAsync(exception);
        }

        public async Task ConnectRoom(string roomId)
        {
            var user = await _userTracker.GetUser(Context.Connection);

            if (user != null)
                await _userTracker.RemoveUser(Context.Connection);

            if (await _userTracker.UserExistsInRoom(Context.Connection, roomId))
                return;

            await _userTracker.AddUser(Context.Connection, new ChatUserDetails
            {
                RoomId = roomId,
                UserId = Context.Connection.User.GetUserId(),
                UserName = Context.Connection.User.Identity.Name
            });

            var usersOnline = await _userTracker.UsersOnline();

            await SendRoomUsers(roomId, usersOnline);
            await SendRoomsOnlineUsers(usersOnline);
        }

        public async Task Send(string roomId, string message)
        {
            var onlineUsers = await _userTracker.UsersOnline();
            var roomUsers = onlineUsers.Where(x => x.RoomId == roomId).ToList();

            foreach (var user in roomUsers)
            {
                var context = await _userTracker.GetUserContext(user);
                await Clients.Client(context.ConnectionId).InvokeAsync("Message", message, Context.User.Identity.Name);
            }
        }

        private async Task SendRoomUsers(string roomId, IList<ChatUserDetails> usersOnline)
        {
            await Clients.All.InvokeAsync("RoomUsers", usersOnline.Where(x => x.RoomId == roomId));
        }

        private async Task SendRoomsOnlineUsers(IList<ChatUserDetails> usersOnline)
        {
            var allRooms = _bus.Handle(new GetRoomsQuery());

            var roomsDetails = usersOnline
                .GroupBy(x => x.RoomId)
                .Select(x => new ChatRoomDetails
                {
                    RoomId = x.Key,
                    UsersOnline = x.Count()
                })
                .ToList();

            foreach (var room in allRooms)
            {
                if (roomsDetails.FirstOrDefault(x => x.RoomId == room.Id) == null)
                {
                    roomsDetails.Add(new ChatRoomDetails
                    {
                        RoomId = room.Id,
                        UsersOnline = 0
                    });
                }
            }

            await Clients.All.InvokeAsync("RoomsOnlineUsers", roomsDetails);
        }
    }
}