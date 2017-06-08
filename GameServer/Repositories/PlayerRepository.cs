using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameServer.Players;
using Microsoft.EntityFrameworkCore;

namespace GameServer.Repositories
{
  public class PlayerRepository
  {
    /// <summary>
    /// Инкапсулирует логику работы с БД
    /// </summary>
    private PlayerContext context;

    public PlayerRepository(PlayerContext context)
    {
      this.context = context;
    }
    /// <summary>
    /// Обрабатывает JsonResult потому, что требуется указать причину ошибки, а также добавить и созранить InfoPlayer в БД
    /// </summary>
    /// <param name="nickname"></param>
    /// <returns></returns>
    public async Task<JsonResult> RegisterAsync(string nickname)
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

    /// <summary>
    /// Просто ищет в БД нужную запить с InfoPlayer или возвращает null
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public async Task<InfoPlayer> LoginAsync(string login)
    {
      return await context.Players.SingleOrDefaultAsync(x => x.Login == login);
    }
  }
}
