using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Players
{
  public class InfoPlayer
  {
    public int Id { get; set; }
    public string Login { get; set; }
    public string Nickname { get; set; }
    public int Wins { get; set; }
  }
}
