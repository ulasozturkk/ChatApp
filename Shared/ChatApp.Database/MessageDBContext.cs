using ChatApp.Models.Mobile;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Database;
public class MessageDbContext : DbContext {

  public MessageDbContext(DbContextOptions<MessageDbContext> options) : base(options) { }

  public DbSet<Message>? messages { get; set; }
  public DbSet<ChatRoom>? chatRooms { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder) {

    modelBuilder.Entity<Message>().HasKey(x => x.ID);
    modelBuilder.Entity<Message>().HasOne(x => x.ChatRoom).WithMany(x => x.Messages).HasForeignKey(x => x.ChatRoomID);
  }
 

}
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MessageDbContext> {
  public MessageDbContext CreateDbContext(string[] args) {
    var optionsBuilder = new DbContextOptionsBuilder<MessageDbContext>();
    optionsBuilder.UseSqlServer("Server=DESKTOP-EH0CS0L;Database=messageDb;Integrated Security=True;TrustServerCertificate=True;");


    return new MessageDbContext(optionsBuilder.Options);
  }
}

