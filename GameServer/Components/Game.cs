using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using GameServer.Repositories;

namespace GameServer.Components
{
  public class Game
  {
    private GameRepository repository;

    public Game(GameRepository repository)
    {
      this.repository = repository;
    }

    public async Task<JsonResult> LoginAsync(string login)
    {
      return await repository.LoginAsync(login);
    }

    public async Task<JsonResult> RegisterAsync(string nickname)
    {
      return await repository.RegisterAsync(nickname);
    }

    public async Task<JsonResult> MenuAsync(string token)
    {
      var player = await repository.GetPlayerByToken(token);

      if(player == null)
      {
        return new JsonResult(new { Success = "bad token" });
      }

      return new JsonResult(new { Success = "ok", Nickname = player.Info.Nickname, Wins = player.Info.Wins });
    }
  }
}
