using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using grey.sh_Server.Controllers;
using System.Collections.Concurrent;

namespace grey.sh_Server.Components
{
  public class Matchmaker
  {
    private ConcurrentDictionary<string, BattleHub> battles = new ConcurrentDictionary<string, BattleHub>();
    private List<GamePlayer> playerQueue = new List<GamePlayer>();
    private bool active;

    public void Start()
    {
      active = true;
      Task.Factory.StartNew(async () =>
      {
        var rnd = new Random();
        while (active)
        {
          if (playerQueue.Count >= 2)
          {
            var index = rnd.Next(0, playerQueue.Count);
            var player1 = playerQueue[index];
            playerQueue.Remove(player1);

            index = rnd.Next(0, playerQueue.Count);
            var player2 = playerQueue[index];
            playerQueue.Remove(player2);

            var battleToken = Guid.NewGuid().ToString().Replace("-", "");

            player1.BattleToken = player2.BattleToken = battleToken;

            var battlePlayer1 = new BattlePlayer
            {
              GamePlayer = player1, //TODO: etc.
            };

            var battlePlayer2 = new BattlePlayer
            {
              GamePlayer = player2 //TODO: etc.
            };

            var battleHub = new BattleHub
            {
              Player1 = battlePlayer1,
              Player2 = battlePlayer2
            };

            battles.TryAdd(battleToken, battleHub);
          }
          await Task.Delay(1000).ConfigureAwait(false);
        }
      });
    }

    public async Task<string> AddPlayerAsync(GamePlayer player)
    {
      player.SearchingForBattle = true;
      playerQueue.Add(player);

      while (player.BattleToken == null && player.SearchingForBattle)
      {
        await Task.Delay(1000);
      }

      if(!player.SearchingForBattle)
      {
        playerQueue.Remove(player);
      }

      player.SearchingForBattle = false;
      return player.BattleToken;
    }

    public Task<BattleHub> GetBattleHubAsync(string battleToken)
    {
      return Task.Run(() =>
      {
        if (battles.TryGetValue(battleToken, out var hub))
        {
          return hub;
        }

        return null;
      });
    }

    public async Task<BattlePlayer> GetBattlePlayerAsync(string battleToken, string token)
    {
      var hub = await GetBattleHubAsync(battleToken);

      if (hub == null)
      {
        return null;
      }

      if(hub.Player1.GamePlayer.Token == token)
      {
        return hub.Player1;
      }

      if (hub.Player2.GamePlayer.Token == token)
      {
        return hub.Player2;
      }

      return null;
    }

    public async Task<BattlePlayer> GetOtherBattlePlayerAsync(string battleToken, string token)
    {
      var hub = await GetBattleHubAsync(battleToken);

      if (hub == null)
      {
        return null;
      }

      if (hub.Player1.GamePlayer.Token == token)
      {
        return hub.Player2;
      }

      if (hub.Player2.GamePlayer.Token == token)
      {
        return hub.Player1;
      }

      return null;
    }

    public void Stop()
    {
      active = false;
    }
  }
}
