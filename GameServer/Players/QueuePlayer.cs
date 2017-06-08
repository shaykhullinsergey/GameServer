namespace GameServer.Players
{
  internal class QueuePlayer
  {
    public GamePlayer Player { get; set; }
    public string BattleToken { get; set; }
    public bool SearchingForBattle { get; set; }
  }
}