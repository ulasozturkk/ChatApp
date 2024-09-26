using ChatApp.Models.Mobile;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.MobileAPI.Database {
  public class MessageDbContext : DbContext {

    public MessageDbContext(DbContextOptions<MessageDbContext> options) : base(options) { }

    public DbSet<Message> messages { get; set; }
    public DbSet<ChatRoom> chatRooms { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder) {

    modelBuilder.Entity<Message>().HasKey(x => x.ID);
    modelBuilder.Entity<Message>().HasOne(x => x.ChatRoom).WithMany(x => x.Messages).HasForeignKey(x => x.ChatRoomID);
  }

}
}
