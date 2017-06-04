using grey.sh_Server.Contexts;
using grey.sh_Server.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace grey.sh_Server.Components
{
  public class GameRepository
  {
    private PlayerRepository players;
    private ConcurrentDictionary<string, GamePlayer> online = new ConcurrentDictionary<string, GamePlayer>();

    public async Task<JsonResult> LoginPlayerAsync(string login)
    {
      var playerData = await players.LoginPlayerAsync(login);

      if (playerData == null)
      {
        return new JsonResult(new { Success = "bad login" });
      }

      var gamePlayer = new GamePlayer
      {
        Player = playerData,
        Token = Guid.NewGuid().ToString().Replace("-", ""),
        BattleToken = null,
        SearchingForBattle = false
      };

      online.TryAdd(gamePlayer.Token, gamePlayer);

      return new JsonResult(new { Success = "ok", Token = gamePlayer.Token });
    }

    public async Task<JsonResult> RegisterPlayerAsync(string nickname)
    {
      return await players.RegisterPlayerAsync(nickname);
    }

    public async Task<JsonResult> PlayerMenuAsync(string token)
    {
      var gamePlayer = await GetPlayerAsync(token);
      if(gamePlayer != null)
      {
        var nickname = gamePlayer.Player.Nickname;
        var wins = gamePlayer.Player.Wins;
        return new JsonResult(new { Success = "ok", Nickname = nickname, Wins = wins });
      }

      return new JsonResult(new { Success = "bad token" });
    }

    public Task<GamePlayer> GetPlayerAsync(string token)
    {
      return Task.Run(() =>
      {
        if (online.TryGetValue(token, out var gamePlayer))
        {
          return gamePlayer;
        }

        return null;
      });
    }
  }
}
