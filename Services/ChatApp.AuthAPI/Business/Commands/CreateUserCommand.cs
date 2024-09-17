using ChatApp.AuthAPI.Dtos;
using ChatApp.Database;
using ChatApp.Models.Mobile;
using ChatApp.Utils.Dtos;
using ChatApp.Utils.Security.JWT;
using ChatApp.Utils.Security.Services.Abstract;
using ChatApp.Utils.Security.Services.Concrete;
using MediatR;
using Microsoft.Extensions.Options;

namespace ChatApp.AuthAPI.Business.Commands; 
public class CreateUserCommand : IRequest<ResponseDTO<UserDto>> {

  public string? username { get; set; }
  public string? password { get; set; }
  public string? verifyPassword { get; set; }

  public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ResponseDTO<UserDto>> {

    private readonly MessageDbContext _db;
    private ISecurityService _securityService;
    private readonly JWTSettings _jwtSettings;
    public CreateUserCommandHandler(MessageDbContext db,ISecurityService securityService,IOptions<JWTSettings> jwtsettings) {
      _db = db;
      _securityService = securityService;
      _jwtSettings = jwtsettings.Value;
    }
    public async Task<ResponseDTO<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken) {
      
      try {
        if (request.username == null || request.verifyPassword == null || request.password == null) {
          return ResponseDTO<UserDto>.Fail("alanları doldur", 400);
        }

        if (request.password == request.verifyPassword) {
          var hashed = _securityService.HashPassword(request.password, request.username, "ulasfather272727");

          var user = new User {
            ID = Guid.NewGuid().ToString(),
            CreatedDate = DateTime.Now.ToString(),
            IsDeleted = false,
            UserName = request.username,
            Password = hashed,
            PushToken = ""
          };

         await _db.users.AddAsync(user);
          await _db.SaveChangesAsync(cancellationToken);

          var userToken = JwtHelper.GetToken(new JwtUserDTO {
            UserID = user.ID,
            UserName = user.UserName,
            Key = _jwtSettings.Key,
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
          });

          var userdto = new UserDto {
            userID = user.ID,
            userName = user.UserName,
            Token = userToken
          };

          return ResponseDTO<UserDto>.Success(userdto, 201);
        } else {
          return ResponseDTO<UserDto>.Fail("şifreler uyuşmuyor", 400);
        }
      } catch {
        return ResponseDTO<UserDto>.Fail("bilinmeyen hata", 400);
      }
    }
  }
}
