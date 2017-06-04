using grey.sh_Server.Controllers;
using Microsoft.EntityFrameworkCore;

namespace grey.sh_Server.Contexts
{
  public class PlayerContext : DbContext
  {
    public DbSet<InfoPlayer> Players { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=False;");
    }
  }
}
