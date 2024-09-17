using ChatApp.Database;
using ChatApp.MobileAPI.Dtos;
using ChatApp.Models.Mobile;
using ChatApp.Utils.Dtos;
using MediatR;

namespace ChatApp.MobileAPI.Business.Commands;
public class CreateChatRoomCommand : IRequest<ResponseDTO<ChatRoomDTO>> {

  public string? chatRoomID { get; set; }

  public class CreateChatRoomCommandHandler : IRequestHandler<CreateChatRoomCommand, ResponseDTO<ChatRoomDTO>> {

    private readonly MessageDbContext _db;

    public CreateChatRoomCommandHandler(MessageDbContext db) {
      _db = db;
    }

    public async Task<ResponseDTO<ChatRoomDTO>> Handle(CreateChatRoomCommand request, CancellationToken cancellationToken) {
      try {
        if (string.IsNullOrEmpty(request.chatRoomID)) {
          return ResponseDTO<ChatRoomDTO>.Fail("alanı doldur", 400);
        }

        await _db.chatRooms.AddAsync(new ChatRoom { ID = request.chatRoomID, Users = null, Messages = null, CreatedDate = DateTime.Now.ToString(), IsDeleted = false });
        await _db.SaveChangesAsync(cancellationToken);

        var dto = new ChatRoomDTO { chatRoomID = request.chatRoomID };

        return ResponseDTO<ChatRoomDTO>.Success(dto, 200);

      } catch (Exception ex) {
        return ResponseDTO<ChatRoomDTO>.Fail("alanı doldur", 400);

      }
    }
  }
}
