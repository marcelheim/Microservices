using Database.Data;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.SeedData;

public class SeedData {
  public static void Initialize(IServiceProvider serviceProvider)
  {
    using var context = new DatabaseContext(serviceProvider.GetRequiredService<DbContextOptions<DatabaseContext>>());

    if (!context.Users.Any()) context.Users.Add(new User()
    {
      Username = "tobi",
      Password = "ilovebbc"
    });

    if(!context.Secrets.Any()) context.Secrets.Add(new Secret()
    {
      Text = "Denn das Wort Gottes ist lebendig und kräftig und schärfer als jedes zweischneidige Schwert und dringt durch, bis dass es scheidet Seele und Geist, auch Mark und Bein, und ist ein Richter der Gedanken und Sinne des Herzens"
    });

    context.SaveChanges();
  }
}