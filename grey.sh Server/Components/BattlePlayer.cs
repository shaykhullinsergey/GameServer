using grey.sh_Server.Models;
using System.Collections.Generic;

namespace grey.sh_Server.Components
{
  public class BattlePlayer
  {
    public bool TurnEnded { get; set; }
    public GamePlayer GamePlayer { get; set; }

    public List<Position> Positions { get; set; }

    //TODO: Other stats for game
  }
}