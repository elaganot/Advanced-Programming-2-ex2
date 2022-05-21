using System.Threading.Tasks;
using E_Chat.Models;
using Microsoft.AspNetCore.SignalR;

namespace E_Chat.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
