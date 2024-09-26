

using ChatApp.Models.Mobile;
using Microsoft.EntityFrameworkCore;
namespace ChatApp.AuthAPI.Database; 
public class UserDbContext : DbContext{

    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

    public DbSet<User> users { get; set; }
  }

