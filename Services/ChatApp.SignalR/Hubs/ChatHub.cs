using ChatApp.SignalR.Dtos;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.SignalR.Hubs; 
public class ChatHub : Hub {
  public async Task SendMessageAsync(MessageDTO message) {
    await Clients.All.SendAsync("receiveMessage", message);

  }

}
