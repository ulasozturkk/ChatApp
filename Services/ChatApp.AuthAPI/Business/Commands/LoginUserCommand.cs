using ChatApp.AuthAPI.Dtos;
using ChatApp.Database;
using ChatApp.Utils.Dtos;
using ChatApp.Utils.Security.JWT;
using ChatApp.Utils.Security.Services.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ChatApp.AuthAPI.Business.Commands; 
public class LoginUserCommand : IRequest<ResponseDTO<UserDto>>{

    public string? username { get; set; }
    public string? password { get; set; }

  public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand,ResponseDTO<UserDto>> {

    private MessageDbContext _db;
    private JWTSettings _jwtSettings;
    private ISecurityService _securityService;

    public LoginUserCommandHandler(MessageDbContext db,IOptions<JWTSettings> jwtSettings,ISecurityService securityService) {
      _db = db;
      _jwtSettings = jwtSettings.Value;
      _securityService = securityService;
    }

    public async Task<ResponseDTO<UserDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken) {
      try {
        if(string.IsNullOrEmpty(request.username) || string.IsNullOrEmpty(request.username)) {
          return  ResponseDTO<UserDto>.Fail("alanları doldur", 400);
        }

        var existUser = await _db.users.Where(x => x.UserName == request.username).FirstOrDefaultAsync();
        if (existUser == null) { 
          return  ResponseDTO<UserDto>.Fail("kullanıcı bulunamadı", 400);
        }
        var ph = _securityService.HashPassword(request.password,request.username, "ulasfather272727");
        var requestHash = _securityService.HashPassword(request.password, request.username, "ulasfather272727");

        if(ph != requestHash) {
          return  ResponseDTO<UserDto>.Fail("bilgiler yanlış", 400);
        }
        var token = JwtHelper.GetToken(new JwtUserDTO { Key = _jwtSettings.Key, Issuer = _jwtSettings.Issuer, Audience = _jwtSettings.Audience, UserName = request.username, UserID = existUser.ID });
        var userdto = new UserDto {
          userName = request.username,
          userID = existUser.ID,
          Token = token

        };
        return ResponseDTO<UserDto>.Success(userdto, 200);


      } catch (Exception ex) {
        return ResponseDTO<UserDto>.Fail("bilinmeyen hata", 400);

      }
    }
  }
}
