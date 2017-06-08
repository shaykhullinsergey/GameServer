using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using GameServer.Core;

namespace GameServer.Components
{
  public partial class Battle
  {
    private Matchmaker matchmaker;

    public Battle(Matchmaker matchmaker)
    {
      this.matchmaker = matchmaker;
      this.matchmaker.Start();
    }

    /// <summary>
    /// Выдача результата проброшена в матчмейкер, потому что не понялтно был маст отменен или плохой токен
    /// Матчи будут добавляться спомощью вложенного класса в приватное статическое поле
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<JsonResult> SearchBattleAsync(string token)
    {
      return await matchmaker.SearchBattleAsync(token);
    }

    /// <summary>
    /// Пробрасываю в матчмейкер, по скольку это тупиковая ветка
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<JsonResult> CancelBattleAsync(string token)
    {
      return await matchmaker.CancelBattleAsync(token);
    }

    /// <summary>
    /// Пробрасывать это в матчмейкер нет смысла, так как все игроки в сражении уже есть в словаре battles
    /// </summary>
    /// <param name="battleToken"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<JsonResult> PrepareBattleAsync(string battleToken, string token)
    {
      var hub = await matchmaker.GetBattleHubAsync(battleToken);
      if (hub == null)
      {
        return new JsonResult(new { Success = "bad battleToken" });
      }

      var otherPlayer = await hub.GetOtherPlayerAsync(token);

      if (otherPlayer == null)
      {
        return new JsonResult(new { Success = "bad token" });
      }

      return new JsonResult(new { Success = "ok", Nickname = otherPlayer.GamePlayer.Info.Nickname });
    }

    /// <summary>
    /// Пробрасывать это в матчмейкер нет смысла, так как все игроки в сражении уже есть в словаре battles
    /// </summary>
    /// <param name="battleToken"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<JsonResult> EndTurnAsync(string battleToken, string token, int p1X, int p1Y, int p2X, int p2Y, int p3X, int p3Y)
    {
      var hub = await matchmaker.GetBattleHubAsync(battleToken);
      if(hub == null)
      {
        return new JsonResult(new { Success = "bad battleToken" });
      }

      var player = await hub.GetPlayerAsync(token);
      if(player == null)
      {
        return new JsonResult(new { Success = "bad token" });
      }

      var positions = new List<Position>
      {
        new Position(p1X, p1Y),
        new Position(p2X, p2Y),
        new Position(p3X, p3Y)
      };

      var otherPlayer = await hub.GetOtherPlayerAsync(token);
      if (otherPlayer == null)
      {
        return new JsonResult(new { Success = "bad token" });
      }

      return await hub.EndTurnAsync(player, otherPlayer, positions);
    }
  }
}
