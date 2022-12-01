using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Data;

public class DatabaseContext: DbContext {
  public DatabaseContext(DbContextOptions<DatabaseContext> options)
    : base(options)
  {
  }

  public DbSet<User> Users { get; set; } = default!;
  public DbSet<Secret> Secrets { get; set; } = default!;
}