using Microsoft.AspNetCore.SignalR;

namespace Chat_Application_WEB.Hubs
{
    public class UserHub : Hub
    {
        public static int TotalViews { get; set; } = 0;

        public static int TotalUsers { get; set; } = 0;

        public async Task NewWindowLoaded()
        {
            TotalViews++;
            //send update to all clients
            await Clients.All.SendAsync("updateTotalViews", TotalViews);
        }

        public override Task OnConnectedAsync()
        {
            TotalUsers++;
            Clients.All.SendAsync("updateTotalUsers", TotalUsers).GetAwaiter();
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            TotalUsers--;
            Clients.All.SendAsync("updateTotalUsers", TotalUsers).GetAwaiter();
            return base.OnDisconnectedAsync(exception);
        }
    }
}