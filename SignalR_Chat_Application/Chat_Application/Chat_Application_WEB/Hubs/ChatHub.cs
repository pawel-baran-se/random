using Chat_Application_WEB.Data;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Chat_Application_WEB.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _context;

        public ChatHub(ApplicationDbContext context)
        {
            _context = context;
        }

        public override Task OnConnectedAsync()
        {
            var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userId))
            {
                var userName = _context.Users.FirstOrDefault(u => u.Id == userId).UserName;
                Clients.Users(HubConnections.OnlineUsers()).SendAsync("ReceiveUserConnected", userId, userName);

                HubConnections.AddUserConnection(userId, Context.ConnectionId);
            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (HubConnections.HasUserConnection(userId, Context.ConnectionId))
            {
                var userConnections = HubConnections.Users[userId];

                userConnections.Remove(Context.ConnectionId);

                HubConnections.Users.Remove(userId);
                if (userConnections.Any())
                {
                    HubConnections.Users.Add(userId, userConnections);
                }
            }

            if (!string.IsNullOrEmpty(userId))
            {
                var userName = _context.Users.FirstOrDefault(u => u.Id == userId).UserName;
                Clients.Users(HubConnections.OnlineUsers()).SendAsync("ReceiveUserDisconnected", userId, userName);
                HubConnections.AddUserConnection(userId, Context.ConnectionId);
            }

            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendAddRoomMessage(int maxRoom, int roomId, string roomName)
        {
            var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = _context.Users.FirstOrDefault(u => u.Id == userId).UserName;

            await Clients.All.SendAsync("ReceiveAddRoomMessage", maxRoom, roomId, roomName, userId, userName);
        }

        public async Task SendDeleteRoomMessage(string roomName)
        {
            var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = _context.Users.FirstOrDefault(u => u.Id == userId).UserName;

            await Clients.All.SendAsync("ReceiveDeleteRoomMessage", roomName, userId, userName);
        }


        public async Task SendPublicMessage(int roomId, string message, string roomName)
        {
            var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = _context.Users.FirstOrDefault(u => u.Id == userId).UserName;

            await Clients.All.SendAsync("ReceivePublicMessage", roomId, userId, userName, message, roomName);
        }

        public async Task SendPrivateMessage(string receiverId, string message, string receiverName)
        {
            var senderId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var senderName = _context.Users.FirstOrDefault(u => u.Id == senderId).UserName;

            var users = new String[] { receiverId, senderId };

            await Clients.Users(users).SendAsync("ReceivePrivvateMessage", senderId, senderName, message, Guid.NewGuid(), receiverName);
        }


        //public async Task SendMessageToAll(string user, string message)
        //{
        //    var hour = DateTime.Now;
        //    var hourToSent = hour.ToString("HH:mm");
        //    await Clients.All.SendAsync("MessageReceived", user, message, hourToSent);
        //}

        //public async Task SendMessageToReceiver(string user, string receiver, string message)
        //{
        //    var hour = DateTime.Now;
        //    var hourToSent = hour.ToString("HH:mm");

        //    var senderId = _context.Users.FirstOrDefault(u => u.Email.ToLower() == receiver.ToLower()).Id;

        //    if (!string.IsNullOrEmpty(senderId))
        //    {
        //        await Clients.User(senderId).SendAsync("MessageReceived", user, message, hourToSent);
        //    }
        //}
    }
}