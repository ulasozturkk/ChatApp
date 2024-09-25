using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Models.dtos {
  public class MessageDTO {
        public string? ChatRoomID { get; set; }
        public string? SenderUserName { get; set; }
    public string? SenderUserID { get; set; }
    public string? MessageText { get; set; }
  }
}
