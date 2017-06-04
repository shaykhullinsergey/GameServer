using grey.sh_Server.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grey.sh_Server.Components
{
  public class Battle
  {
    public Matchmaker Matchmaker { get; set; }

    public async Task<JsonResult> EndTurnAsync(string battleToken, string token, int p1X, int p1Y, int p2X, int p2Y, int p3X, int p3Y)
    {
      var hub = await Matchmaker.GetBattleHub(battleToken);
      var player = await Matchmaker.GetBattlePlayer(battleToken, token);

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
      var positions = await hub.WaitForEndTurn();

      return new JsonResult(new { Success = "ok", Positions = positions });
    }
  }
}
