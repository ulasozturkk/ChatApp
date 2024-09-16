using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Utils.Security.JWT; 
public class JwtHelper {
  public static string GetToken(JwtUserDTO request) {
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(request.Key));
    var credientals = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

    var claims = new List<Claim> {
      new Claim(JwtRegisteredClaimNames.Jti,request.UserName ?? ""),
      new Claim("USERID", $"{request.UserID}" ?? "")

    };
    var token = new JwtSecurityToken(request.Issuer,request.Audience,claims:claims,expires:DateTime.Now.AddHours(3),signingCredentials:credientals);

    return new JwtSecurityTokenHandler().WriteToken(token);

  }
}
