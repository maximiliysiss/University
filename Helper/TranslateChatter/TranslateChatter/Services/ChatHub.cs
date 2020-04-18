using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using TranslateChatter.Models;

namespace TranslateChatter.Services
{
    [Authorize]
    public class ChatHub : Hub
    {
        public string Room => Context?.GetHttpContext()?.Request?.Query["room"].ToString();


        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            var room = Room;
            if (!string.IsNullOrEmpty(room))
                await Groups.AddToGroupAsync(Context.ConnectionId, room);
        }

        public async Task Send(string message)
        {
            await Clients.Group(Room).SendAsync("Send", message, Context.User.Identity.Name);
        }
    }
}
