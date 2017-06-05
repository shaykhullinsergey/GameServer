using grey.sh_Server.Models;
using System.Collections.Generic;
using System;

namespace grey.sh_Server.Components
{
  public class BattlePlayer
  {
    public bool TurnEnded { get; set; }
    public GamePlayer GamePlayer { get; set; }

    public List<Position> Positions { get; set; }

    //TODO: Other stats for games

    public void EndTurn(int p1X, int p1Y, int p2X, int p2Y, int p3X, int p3Y)
    {
      Positions[0].X = p1X;
      Positions[0].Y = p1Y;

      Positions[1].X = p1X;
      Positions[1].Y = p2Y;

      Positions[2].X = p3X;
      Positions[2].Y = p3Y;

      TurnEnded = true;
    }

  }
}