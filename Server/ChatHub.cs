using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR;

namespace SignalR_Example.Server;

public class ChatHub : Hub
{
    public async Task Send(string userName, string message)
    {
        await Clients.All.SendAsync("Receive", userName, message);
    }
}
