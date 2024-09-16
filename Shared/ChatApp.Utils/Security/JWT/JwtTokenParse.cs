using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ChatApp.Utils.Security.JWT;
public class JwtTokenParse : IJwtTokenParse {

  private IHttpContextAccessor _httpContextAncessor;

  public JwtTokenParse(IHttpContextAccessor httpContextAncessor) {
    _httpContextAncessor = httpContextAncessor;
  }

  public string? GetUserID() {
    string? userID = null;
    if(_httpContextAncessor == null || _httpContextAncessor.HttpContext == null) {
      return "";
    }
    var identity = _httpContextAncessor.HttpContext.User;
    var id = identity.Claims.SingleOrDefault(c => c.Type == "USERID");

    if (id != null) {
      userID = id.Value;

    }
    return userID;

  }

  public string? GetUserName() {
    string? userName = null;
    if(_httpContextAncessor == null || _httpContextAncessor.HttpContext == null) {
      return "";
    }
    var identity = _httpContextAncessor.HttpContext.User;
    var username = identity.Claims.SingleOrDefault(c => c.Type == "USERNAME");

    if(username != null) {
      userName = username.Value;
    }
    return userName;
  }
}
