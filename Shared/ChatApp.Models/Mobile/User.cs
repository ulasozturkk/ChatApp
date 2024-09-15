using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Models.Mobile {
  public class User:BaseModel {

        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Token { get; set; }
        public string? PushToken { get; set; } //user's device push token for firebase messaging
    }
}
