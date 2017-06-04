using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using grey.sh_Server.Components;
using grey.sh_Server.Contexts;
using Microsoft.EntityFrameworkCore;

namespace grey.sh_Server.Controllers
{
  [Route("/")]
  public class GameController : Controller
  {
    private Game game;

    [HttpGet("Connect")]
    public async Task<IActionResult> Connect()
    {
      return game.Connect();
    }

    [HttpGet("Login/{login}")]
    public async Task<IActionResult> Login(string login)
    {
      return await game.Login(login);
    }

    [HttpGet("Register/{nickname}")]
    public async Task<IActionResult> Register(string nickname)
    {
      return await game.Register(nickname);
    }

    [HttpGet("Menu/{token}")]
    public async Task<IActionResult> Menu(string token)
    {
      return await game.Menu(token);
    }

    [HttpGet("SearchBattle/{token}")]
    public async Task<IActionResult> SearchBattle(string token)
    {
      return await game.SearchBattle(token);
    }

    [HttpGet("CancelBattle/{token}")]
    public async Task<IActionResult> CancelBattle(string token)
    {
      return await game.CancelBattle(token);
    }

    [HttpGet("PrepareBattle/{battleToken}/{token}")]
    public async Task<IActionResult> PrepareBattle(string battleToken, string token)
    {
      return await game.PrepareBattle(battleToken, token);
    }

    [HttpGet("Battle/{battleToken}/{token}/{p1X}/{p1Y}/{p2X}/{p2Y}/{p3X}/{p3Y}")]
    public async Task<IActionResult> Battle(string battleToken, string token, int p1X, int p1Y, int p2X, int p2Y, int p3X, int p3Y)
    {
      var hub = battle.ActiveHubs[battleToken];
      var playerData = await hub.WaitForResultAsync(token, p1X, p1Y, p2X, p2Y, p3X, p3Y);

      if (playerData == null)
      {
        return Json(new { Success = "bad token" });
      }

      //TODO: Conditions

      return Json(new { Success = "ok", PlayerData = playerData });
    }
  }
}
