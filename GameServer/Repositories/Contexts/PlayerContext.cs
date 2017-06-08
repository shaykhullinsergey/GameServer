using GameServer.Players;
using Microsoft.EntityFrameworkCore;

namespace GameServer.Repositories
{
  public class PlayerContext : DbContext
  {
    public DbSet<InfoPlayer> Players { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=GameServer;Trusted_Connection=False;");
    }
  }
}