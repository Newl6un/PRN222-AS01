using Microsoft.AspNetCore.SignalR;

namespace LeHoangNhatTanRazorPages.Services.Hubs
{
    public class NewsHub : Hub
    {
        public async Task SendNewsUpdate(string message)
        {
            await Clients.All.SendAsync("ReceiveNewsUpdate", message);
        }

        public async Task SendNewsEdit(string message)
        {
            await Clients.All.SendAsync("ReceiveNewsEdit", message);
        }

        public async Task SendNewsDelete(string message)
        {
            await Clients.All.SendAsync("ReceiveNewsDelete", message);
        }
    }
}
