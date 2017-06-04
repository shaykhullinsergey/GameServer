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
      return await game.LoginAsync(login);
    }

    [HttpGet("Register/{nickname}")]
    public async Task<IActionResult> Register(string nickname)
    {
      return await game.RegisterAsync(nickname);
    }

    [HttpGet("Menu/{token}")]
    public async Task<IActionResult> Menu(string token)
    {
      return await game.MenuAsync(token);
    }

    //TODO: Battle/Search
    [HttpGet("SearchBattle/{token}")]
    public async Task<IActionResult> SearchBattle(string token)
    {
      return await game.SearchBattleAsync(token);
    }

    //TODO: Battle/Cancel
    [HttpGet("CancelBattle/{token}")]
    public async Task<IActionResult> CancelBattle(string token)
    {
      return await game.CancelBattleAsync(token);
    }

    //TODO: Battle/Prepare
    [HttpGet("PrepareBattle/{battleToken}/{token}")]
    public async Task<IActionResult> PrepareBattle(string battleToken, string token)
    {
      return await game.PrepareBattleAsync(battleToken, token);
    }

    //TODO: Battle/EndTurn
    [HttpGet("Battle/{battleToken}/{token}/{p1X}/{p1Y}/{p2X}/{p2Y}/{p3X}/{p3Y}")]
    public async Task<IActionResult> EndTurn(string battleToken, string token, int p1X, int p1Y, int p2X, int p2Y, int p3X, int p3Y)
    {
      return await game.EndTurnAsync(battleToken, token, p1X, p1Y, p2X, p2Y, p3X, p3Y);
    }
  }
}
