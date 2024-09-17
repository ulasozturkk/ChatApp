using AutoMapper;
using AutoMapper.Internal.Mappers;
using ChatApp.Database;
using ChatApp.MobileAPI.Dtos;
using ChatApp.MobileAPI.Mapping;
using ChatApp.Utils.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.MobileAPI.Business.Queries; 
public class GetMessagesByChatRoomID : IRequest<ResponseDTO<List<MessageDTO>>> {

    public string? chatRoomID { get; set; }
    public class GetMessagesByChatRoomIDHandler : IRequestHandler<GetMessagesByChatRoomID,ResponseDTO<List<MessageDTO>>> {

    private readonly MessageDbContext _db;
    private readonly IMapper _mapper;

    public GetMessagesByChatRoomIDHandler(MessageDbContext db,IMapper mapper) {
      _db = db;
      _mapper = mapper;
    }

    public async Task<ResponseDTO<List<MessageDTO>>> Handle(GetMessagesByChatRoomID request, CancellationToken cancellationToken) {
      try {

        if (string.IsNullOrEmpty(request.chatRoomID)) {
          return ResponseDTO<List<MessageDTO>>.Fail("alanları doldur", 400);      
            }

        var results = await _db.messages.Where(x => x.ChatRoomID == request.chatRoomID).ToListAsync();

        var resultDTO = _mapper.Map<List<MessageDTO>>(results);

        return ResponseDTO<List<MessageDTO>>.Success(resultDTO, 200);
      }catch {
        return ResponseDTO<List<MessageDTO>>.Fail("hata", 500);

      }
    }
  }
}
