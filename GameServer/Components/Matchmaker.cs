using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GameServer.Players;
using GameServer.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GameServer.Components
{
  public class Matchmaker
  {
    private Dictionary<string, BattleHub> battles = new Dictionary<string, BattleHub>();
    private Dictionary<string, QueuePlayer> playerQueue = new Dictionary<string, QueuePlayer>();
    private GameRepository repository;
    private bool active;

    public Matchmaker(GameRepository repository)
    {
      this.repository = repository;
    }

    /// <summary>
    /// Добавляет в очередь нового игрока и ждет пока ему найдут соперника или он выйдет
    /// После выданный BattleToken посылает на клиент
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<JsonResult> SearchBattleAsync(string token)
    {
      var player = await repository.GetPlayerByToken(token);

      if (player == null)
      {
        return new JsonResult(new { Success = "bad token" });
      }

      var queue = new QueuePlayer
      {
        Player = player,
        BattleToken = null,
        SearchingForBattle = true
      };

      playerQueue.Add(token, queue);

      while (queue.BattleToken == null && queue.SearchingForBattle)
      {
        await Task.Delay(1000);
      }

      if (!queue.SearchingForBattle)
      {
        playerQueue.Remove(token);
        return new JsonResult(new { Success = "canceled" });
      }

      queue.SearchingForBattle = false;

      return new JsonResult(new { Success = "ok", BattleToken = queue.BattleToken });
    }

    /// <summary>
    /// Находит в очереди нужного игрока и отменяет его поиск
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<JsonResult> CancelBattleAsync(string token)
    {
      return Task.Run(() =>
      {
        if (playerQueue.TryGetValue(token, out var queue))
        {
          queue.SearchingForBattle = false;
          return new JsonResult(new { Success = "ok" });
        }

        return new JsonResult(new { Success = "bad token" });
      });
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
    /// <summary>
    /// Цикл, в котором подбираются соперники, выдается токен и создается хаб
    /// </summary>
    public void Start()
    {
      active = true;
      Task.Factory.StartNew(async () =>
      {
        var rnd = new Random();
        while (active)
        {
          while (playerQueue.Count >= 2)
          {
            var index = rnd.Next(0, playerQueue.Count);
            var queue1 = playerQueue.Values.ElementAt(index);
            playerQueue.Remove(queue1.Player.Token);

            index = rnd.Next(0, playerQueue.Count);
            var queue2 = playerQueue.Values.ElementAt(index);
            playerQueue.Remove(queue2.Player.Token);

            var battleToken = Guid.NewGuid().ToString().Replace("-", "");
            queue1.BattleToken = queue2.BattleToken = battleToken;

            var battlePlayer1 = new BattlePlayer
            {
              GamePlayer = queue1.Player //TODO: etc.
              };

            var battlePlayer2 = new BattlePlayer
            {
              GamePlayer = queue2.Player //TODO: etc.
              };

            var battleHub = new BattleHub(battlePlayer1, battlePlayer2);
            battles.Add(battleToken, battleHub);
          }
          await Task.Delay(1000).ConfigureAwait(false);
        }
      });
    }
  }
}