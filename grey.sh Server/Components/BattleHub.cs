using grey.sh_Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grey.sh_Server.Components
{
  public class BattleHub
  {
    public BattlePlayer Player1 { get; set; }
    public BattlePlayer Player2 { get; set; }

    public bool TurnEnded => Player1.TurnEnded && Player2.TurnEnded;

    public async Task<IEnumerable<Position>> WaitForEndTurn()
    {
      while (TurnEnded)
      {
        await Task.Delay(1000);
      }

      return Player1.Positions.Concat(Player2.Positions);
    }
  }
}