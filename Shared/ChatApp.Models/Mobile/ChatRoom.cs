using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Models.Mobile {
  public class ChatRoom : BaseModel{

        public List<User>? Users { get; set; }
        public List<Message>? Messages { get; set; }
    }
}
