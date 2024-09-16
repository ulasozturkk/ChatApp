using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Utils.Security.JWT; 
public interface IJwtTokenParse {

  public string? GetUserID();
  public string? GetUserName();


}
