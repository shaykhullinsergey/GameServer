using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using GameServer.Core;
using GameServer.Players;

namespace GameServer.Components
{
  public class BattleHub
  {
    private BattlePlayer player1;
    private BattlePlayer player2;

    public bool TurnEnded => player1.TurnEnded && player2.TurnEnded;

    public BattleHub(BattlePlayer player1, BattlePlayer player2)
    {
      this.player1 = player1;
      this.player2 = player2;
    }

    public Task<BattlePlayer> GetPlayerAsync(string token)
    {
      return Task.Run(() =>
      {
        if (player1.GamePlayer.Token == token)
        {
          return player1;
        }

        if (player2.GamePlayer.Token == token)
        {
          return player2;
        }

        return null;
      });
    }

    public Task<BattlePlayer> GetOtherPlayerAsync(string token)
    {
      return Task.Run(() =>
      {
        if(player1.GamePlayer.Token == token)
        {
          return player2;
        }

        if (player2.GamePlayer.Token == token)
        {
          return player1;
        }

        return null;
      });
    }

    public async Task<JsonResult> EndTurnAsync(BattlePlayer player, BattlePlayer otherPlayer, List<Position> positions)
    {
      player.Positions = positions;

      await WaitForNextTurn(player);

      //PLAYER LOGIC!!!
      var otherPositions = otherPlayer.Positions.Select(p => new Position(6 - p.X, 8 - p.Y));
      var allPositions = player.Positions.Concat(otherPositions);
      //TODO: Add collisions
      return new JsonResult(new { Success = "ok", Positions = allPositions });
    }

    private async Task WaitForNextTurn(BattlePlayer player)
    {
      player.TurnEnded = true;
      while (!TurnEnded)
      {
        await Task.Delay(100);
      }
      await Task.Delay(250);
      player.TurnEnded = false;
    }
  }
}
