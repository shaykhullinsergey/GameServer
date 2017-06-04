using grey.sh_Server.Contexts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grey.sh_Server.Components
{
  public class Game
  {
    private Battle battle;
    private GameRepository gamePlayers;

    public JsonResult Connect()
    {
      return new JsonResult(new { Success = "ok" });
    }

    public async Task<JsonResult> LoginAsync(string token)
    {
      return await gamePlayers.LoginPlayerAsync(token);
    }

    public async Task<JsonResult> RegisterAsync(string nickname)
    {
      return await gamePlayers.RegisterPlayerAsync(nickname);
    }

    public async Task<JsonResult> MenuAsync(string token)
    {
      return await gamePlayers.PlayerMenuAsync(token);
    }

    public async Task<JsonResult> SearchBattleAsync(string token)
    {
      var gamePlayer = await gamePlayers.GetPlayerAsync(token);

      if (gamePlayer == null)
      {
        return new JsonResult(new { Success = "bad token" });
      }

      var battleToken = await battle.Matchmaker.AddAsync(gamePlayer);

      return new JsonResult(new { Success = "ok", BattleToken = battleToken });
    }

    public async Task<JsonResult> CancelBattleAsync(string token)
    {
      var gamePlayer = await gamePlayers.GetPlayerAsync(token);

      if (gamePlayer == null)
      {
        return new JsonResult(new { Success = "bad token" });
      }

      gamePlayer.SearchingForBattle = false;

      return new JsonResult(new { Success = "ok" });
    }

    public async Task<JsonResult> PrepareBattleAsync(string battleToken, string token)
    {
      var otherBattlePlayer = await battle.Matchmaker.GetOtherBattlePlayerAsync(battleToken, token);

      if (otherBattlePlayer == null)
      {
        return new JsonResult(new { Success = "bad token" });
      }

      return new JsonResult(new { Success = "ok", Nickname = otherBattlePlayer.GamePlayer.Player.Nickname });
    }
    
  }
}
