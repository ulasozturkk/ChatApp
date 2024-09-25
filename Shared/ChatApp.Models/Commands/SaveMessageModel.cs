using ChatApp.Models.Mobile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Models.Commands {
  public class SaveMessageModel {
    public string? ID { get; set; }
    public string? CreatedDate { get; set; }
    public bool? IsDeleted { get; set; }
    public string? ChatRoomID { get; set; }
    public ChatRoom? ChatRoom { get; set; }
    public string? SenderUserName { get; set; }
    public string? SenderUserID { get; set; }
    public string? MessageText { get; set; }
  }
}
