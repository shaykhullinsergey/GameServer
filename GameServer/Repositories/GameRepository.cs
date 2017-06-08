using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using GameServer.Players;

namespace GameServer.Repositories
{
  public class GameRepository
  {
    /// <summary>
    /// Содержит всех игроков, находящихся в онлайн
    /// </summary>
    private Dictionary<string, GamePlayer> players = new Dictionary<string, GamePlayer>();

    /// <summary>
    /// Инкапсулирует логику создания и работы с InfoPlayer
    /// </summary>
    private PlayerRepository repository;

    public GameRepository(PlayerRepository repository)
    {
      this.repository = repository;
    }

    /// <summary>
    /// Ответ клиенту делегируется в PlayerRepository потому, что нужно знать 
    /// по какой причине не удалось зарегистрироваться и ответственность за InfoPlayer неcет только PlayerRepository
    /// </summary>
    /// <param name="nickname"></param>
    /// <returns></returns>
    public async Task<JsonResult> RegisterAsync(string nickname)
    {
      return await repository.RegisterAsync(nickname);
    }

    /// <summary>
    /// Из PlayerRepository возвращается InfoPlayer и сам код не вынесен в Game 
    /// потому, что новый экземпляр GamePlayer требуется добавить в GameRepository.players
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public async Task<JsonResult> LoginAsync(string login)
    {
      var info = await repository.LoginAsync(login);

      if (info == null)
      {
        return new JsonResult(new { Success = "bad login" });
      }

      var player = new GamePlayer
      {
        Info = info,
        Token = Guid.NewGuid().ToString().Replace("-", "")
      };
      players.Add(player.Token, player);

      return new JsonResult(new { Success = "ok", Token = player.Token });
    }

    /// <summary>
    /// Метод не пробрасывается в PlayerRepository потому, что данные о меню можно достать из онлайна
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<GamePlayer> GetPlayerByToken(string token)
    {
      return Task.Run(() =>
      {
        if(players.TryGetValue(token, out var player))
        {
          return player;
        }

        return null;
      });
    }
  }
}
