using Microsoft.AspNetCore.Mvc;

using GameServer.Components;
using System.Threading.Tasks;

namespace GameServer.Controllers
{
  [Route("[controller]")]
  public class BattleController : Controller
  {
    private Battle battle;

    public BattleController(Battle battle)
    {
      this.battle = battle;
    }

    [HttpGet("Search/{token}")]
    public async Task<JsonResult> Search(string token)
    {
      return await battle.SearchBattleAsync(token);
    }

    [HttpGet("Cancel/{token}")]
    public async Task<JsonResult> Cancel(string token)
    {
      return await battle.CancelBattleAsync(token);
    }

    [HttpGet("Prepare/{battleToken}/{token}")]
    public async Task<JsonResult> Prepare(string battleToken, string token)
    {
      return await battle.PrepareBattleAsync(battleToken, token);
    }

    [HttpGet("EndTurn/{battleToken}/{token}/{p1X}/{p1Y}/{p2X}/{p2Y}/{p3X}/{p3Y}")]
    public async Task<JsonResult> EndTurn(string battleToken, string token, int p1X, int p1Y, int p2X, int p2Y, int p3X, int p3Y)
    {
      return await battle.EndTurnAsync(battleToken, token, p1X, p1Y, p2X, p2Y, p3X, p3Y);
    }
  }
}
