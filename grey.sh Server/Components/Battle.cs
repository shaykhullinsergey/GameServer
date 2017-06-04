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
      var player = await Matchmaker.GetBattlePlayerAsync(battleToken, token);

      if (player == null)
      {
        return new JsonResult(new { Success = "bad token" });
      }

      //TODO: Conditions

      player.Positions[0].X = p1X;
      player.Positions[0].Y = p1Y;

      player.Positions[1].X = p1X;
      player.Positions[1].Y = p2Y;

      player.Positions[2].X = p3X;
      player.Positions[2].Y = p3Y;

      player.TurnEnded = true;

      await hub.WaitForEndTurnAsync();

      var otherPlayer = await Matchmaker.GetOtherBattlePlayerAsync(battleToken, token);

      var positions = player.Positions.Concat(otherPlayer.Positions);

      return new JsonResult(new { Success = "ok", Positions = positions });
    }
  }
}
