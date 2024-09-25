
using ChatApp.Models.Commands;
using ChatApp.Models.dtos;
using ChatApp.Models.Mobile;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.SignalR.Hubs; 
public class ChatHub : Hub {

  private readonly ISendEndpointProvider _sendEndpointProvider;
  public ChatHub(ISendEndpointProvider sendEndpointProvider) { 
    _sendEndpointProvider = sendEndpointProvider;
  }

  public async Task SendMessageAsync(MessageDTO message) {
    await Clients.All.SendAsync("receiveMessage", message);
    var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:save-message-queue"));
    var msg = new SaveMessageModel {
      ID = Guid.NewGuid().ToString(),
      CreatedDate = DateTime.Now.ToString(),
      IsDeleted = false,
      ChatRoomID = message.ChatRoomID,
      SenderUserName = message.SenderUserName,
      SenderUserID = message.SenderUserID,
      MessageText = message.MessageText,
    };
    await endpoint.Send(msg);

  }

}
