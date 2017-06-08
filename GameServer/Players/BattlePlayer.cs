using GameServer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Players
{
  public class BattlePlayer
  {
    public GamePlayer GamePlayer { get; set; }
    public List<Position> Positions { get; set; }
    public bool TurnEnded { get; set; }
  }
}
