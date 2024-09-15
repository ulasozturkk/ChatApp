using ChatApp.Models.Mobile;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Database.Configurations {
  public class MessageConfig : IEntityTypeConfiguration<Message> {


    public void Configure(EntityTypeBuilder<Message> builder) {
      builder.HasKey(x=> x.ID);
      builder.HasOne(x => x.ChatRoom).WithMany(x => x.Messages).HasForeignKey(x => x.ChatRoomID);
    }
  }
}
