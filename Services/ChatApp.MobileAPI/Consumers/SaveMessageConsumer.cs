using ChatApp.Database;
using ChatApp.Models.Commands;
using ChatApp.Models.Mobile;
using MassTransit;
using System.Threading;

namespace ChatApp.MobileAPI.Consumers {
  public class SaveMessageConsumer : IConsumer<SaveMessageModel> {

    private readonly MessageDbContext _db;

    public SaveMessageConsumer(MessageDbContext db) {
      _db = db;
    }

    public async Task Consume(ConsumeContext<SaveMessageModel> context) {
      var message = context.Message;

      var msg = new Message {
        ID = message.ID,
        CreatedDate = message.CreatedDate,
        IsDeleted = message.IsDeleted,
        ChatRoomID = message.ChatRoomID,
        SenderUserID = message.SenderUserID,
        SenderUserName = message.SenderUserName,
        MessageText = message.MessageText,
      };
      await _db.messages.AddAsync(msg);
      await _db.SaveChangesAsync();
    }
  }
}
