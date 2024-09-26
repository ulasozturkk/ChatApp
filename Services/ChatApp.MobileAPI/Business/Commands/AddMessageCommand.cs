
using ChatApp.MobileAPI.Database;
using ChatApp.Models.Mobile;
using ChatApp.Utils.Dtos;
using ChatApp.Utils.Security.JWT;
using ChatApp.Utils.Security.Services.Abstract;
using MediatR;

namespace ChatApp.MobileAPI.Business.Commands; 
public class AddMessageCommand  : IRequest<ResponseDTO<bool>>{

  public string? ChatRoomID { get; set; }
  public string? MessageText { get; set; }

  public class AddMessageCommandHandler : IRequestHandler<AddMessageCommand,ResponseDTO<bool>> {

    private readonly MessageDbContext _db;
    private readonly IJwtTokenParse _jwtTokenParse;

    public AddMessageCommandHandler(MessageDbContext db,  IJwtTokenParse jwtTokenParse) {
      _db = db;
      _jwtTokenParse = jwtTokenParse;
    }

    public async Task<ResponseDTO<bool>> Handle(AddMessageCommand request, CancellationToken cancellationToken) {
      try {

        if(string.IsNullOrEmpty(request.ChatRoomID) || string.IsNullOrEmpty(request.MessageText)) {
          return ResponseDTO<bool>.Fail("alanları doldur", 404);
        }

        var userID = _jwtTokenParse.GetUserID();
        var userName = _jwtTokenParse.GetUserName();

        if (userID == null && userName == null) {
          return ResponseDTO<bool>.Fail("token alınamadı", 404);

        }

        var msg = new Message {
          ChatRoomID = request.ChatRoomID,

          ID = Guid.NewGuid().ToString(),
          SenderUserID = userID,
          SenderUserName = userName,
          MessageText = request.MessageText,
          CreatedDate = DateTime.Now.ToString(),
          IsDeleted = false
        };
        await _db.messages.AddAsync(msg);
        await _db.SaveChangesAsync(cancellationToken);

        return ResponseDTO<bool>.Success(true, 200);
      } catch (Exception ex) {
          return ResponseDTO<bool>.Fail(ex.ToString(), 404);
      }
    }
  }
}
