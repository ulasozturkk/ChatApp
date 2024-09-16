using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Utils; 
public static class Extensions {

  public static string ToMD5(this String text) {
    MD5 md5 = MD5.Create();

    //metnin boyutundan hash hesaplar
    md5.ComputeHash(Encoding.ASCII.GetBytes(text));

    //hesapladıktan sonra hashi alır
    byte[] result = md5.Hash ?? new byte[0];

    StringBuilder strBuilder = new StringBuilder();
    for (int i = 0; i < result.Length; i++) {
      //her baytı 2 hexadecimal hane olarak değiştirir
      strBuilder.Append(result[i].ToString("x2"));
    }

    return strBuilder.ToString();
  }
}
