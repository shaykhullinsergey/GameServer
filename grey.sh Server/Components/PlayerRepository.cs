using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using grey.sh_Server.Contexts;
using Microsoft.EntityFrameworkCore;
using grey.sh_Server.Controllers;

namespace grey.sh_Server.Components
{
  public class PlayerRepository
  {
    private PlayerContext context;

    public PlayerRepository(PlayerContext context)
    {
      this.context = context;
    }

    public async Task<InfoPlayer> LoginPlayerAsync(string login)
    {
      var player = await context.Players.SingleOrDefaultAsync(x => x.Login == login);
      return player;
    }

    public async Task<JsonResult> RegisterPlayerAsync(string nickname)
    {
      if (await context.Players.AnyAsync(x => x.Nickname == nickname))
      {
        return new JsonResult(new { Success = "nickname exists" });
      }

      var player = new InfoPlayer
      {
        Login = Guid.NewGuid().ToString().Replace("-", ""),
        Nickname = nickname,
        Wins = 0
      };

      await context.Players.AddAsync(player);
      await context.SaveChangesAsync();

      return new JsonResult(new { Success = "ok", Login = player.Login });
    }
  }
}
