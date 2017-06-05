using grey.sh_Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grey.sh_Server.Components
{
  public class BattleHub
  {
    private BattlePlayer battlePlayer1;
    private BattlePlayer battlePlayer2;

    public BattleHub(BattlePlayer battlePlayer1, BattlePlayer battlePlayer2)
    {
      this.battlePlayer1 = battlePlayer1;
      this.battlePlayer2 = battlePlayer2;
    }

    private bool TurnEnded => battlePlayer1.TurnEnded && battlePlayer2.TurnEnded;

    public async Task WaitForEndTurnAsync()
    {
      while (!TurnEnded)
      {
        await Task.Delay(1000).ConfigureAwait(false);
      }
    }

    public Task<BattlePlayer> GetBattlePlayerAsync(string token)
    {
      return Task.Run(() =>
      {
        if (battlePlayer1.GamePlayer.Token == token)
        {
          return battlePlayer1;
        }

        if (battlePlayer2.GamePlayer.Token == token)
        {
          return battlePlayer2;
        }

        return null;
      });
    }

    public Task<BattlePlayer> GetOtherBattlePlayerAsync(string token)
    {
      return Task.Run(() =>
      {
        if (battlePlayer1.GamePlayer.Token == token)
        {
          return battlePlayer2;
        }

        if (battlePlayer2.GamePlayer.Token == token)
        {
          return battlePlayer1;
        }

        return null;
      });
    }
  }
}