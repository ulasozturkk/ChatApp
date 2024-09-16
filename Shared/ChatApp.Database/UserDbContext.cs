using ChatApp.Database;
using ChatApp.Models.Mobile;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Database; 
public class UserDbContext : DbContext {

  public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

  public DbSet<User> users { get; set; }
}


