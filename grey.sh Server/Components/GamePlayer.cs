using grey.sh_Server.Controllers;

namespace grey.sh_Server.Components
{
  public class GamePlayer
  {
    public InfoPlayer Player { get; internal set; }

    public string Token { get; internal set; }
    public string BattleToken { get; internal set; }
    public bool SearchingForBattle { get; internal set; }
  }
}