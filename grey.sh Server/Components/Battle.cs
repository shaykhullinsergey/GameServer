using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace grey.sh_Server.Components
{
  public class Battle
  {
    public Matchmaker Matchmaker { get; set; }

    public Battle(Matchmaker matchmaker)
    {
      Matchmaker = matchmaker;
      Matchmaker.Start();
    }

    public async Task<JsonResult> EndTurnAsync(string battleToken, string token, int p1X, int p1Y, int p2X, int p2Y, int p3X, int p3Y)
    {
      var hub = await Matchmaker.GetBattleHubAsync(battleToken);
      if(hub == null)
      {
        return new JsonResult(new { Success = "bad battleToken" });
      }

      var player = await hub.GetBattlePlayerAsync(token);
      if (player == null)
      {
        return new JsonResult(new { Success = "bad token" });
      }

      player.EndTurn(p1X, p1Y, p2X, p2Y, p3X, p3Y);

      //TODO: Conditions

      await hub.WaitForEndTurnAsync();

      var otherPlayer = await Matchmaker.GetOtherBattlePlayerAsync(battleToken, token);

      var positions = player.Positions.Concat(otherPlayer.Positions);

      return new JsonResult(new { Success = "ok", Positions = positions });
    }
  }
}
