using Chat_Application_WEB.Data;
using Microsoft.AspNetCore.SignalR;

namespace Chat_Application_WEB.Hubs
{
    public class BasiChatHub : Hub
    {
        private readonly ApplicationDbContext _context;

        public BasiChatHub(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SendMessageToAll(string user, string message)
        {
            var hour = DateTime.Now;
            var hourToSent = hour.ToString("HH:mm");
            await Clients.All.SendAsync("MessageReceived", user, message, hourToSent);
        }

        public async Task SendMessageToReceiver(string user, string receiver, string message)
        {
            var hour = DateTime.Now;
            var hourToSent = hour.ToString("HH:mm");

            var userId = _context.Users.FirstOrDefault(u => u.Email.ToLower() == receiver.ToLower()).Id;

            if (!string.IsNullOrEmpty(userId))
            {
                await Clients.User(userId).SendAsync("MessageReceived", user, message, hourToSent);
            }
        }
    }
}