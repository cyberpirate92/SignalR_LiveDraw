using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SignalR_LiveDraw {
    public class PositionHub: Hub {

        private static int CurrentUserCount = 0;
        // private static List<string> ConnectionIdList = new List<string>();

        public override async Task OnConnectedAsync() {
            await Clients.All.SendAsync("UserConnected", ++CurrentUserCount, Context.ConnectionId);
            await base.OnConnectedAsync();
        }
        public Task SendPoint(int x, int y) {
            return Clients.All.SendAsync("ReceivePoint", x, y);
        }

        public override async Task OnDisconnectedAsync(System.Exception exception) {
            await Clients.Others.SendAsync("UserDisconnected", --CurrentUserCount, Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }

        public Task SendChatMessage(string message) {
            return Clients.All.SendAsync("ReceiveChatMessage", message, Context.ConnectionId);
        }
    }
}