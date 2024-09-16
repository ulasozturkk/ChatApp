using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Utils.Security.Services.Abstract; 
public interface ISecurityService {
  public string HashCreate(string value, string salt);

  public string HashPassword(string Password, string Username, string Salt);

  public string EncryptToAES(string plainText, string keyString);

  public string DecryptFromAES(string cipherTextStr, string keyString);
}
