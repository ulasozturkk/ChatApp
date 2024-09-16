using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Utils.Security.JWT; 
public class JwtUserDTO {

    public string? UserName { get; set; }
    public string? UserID { get; set; }

  public string? Key { get; set; }
  public string? Issuer { get; set; }
  public string? Audience { get; set; }
}
