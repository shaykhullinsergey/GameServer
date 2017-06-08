using GameServer.Components;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GameServer.Controllers
{
  [Route("[controller]")]
  public class GameController : Controller
  {
    public Game game;

    public GameController(Game game)
    {
      this.game = game;
    }

    [HttpGet("Connect")]
    public JsonResult Connect()
    {
      return Json(new { Success = "ok" });
    }
    
    [HttpGet("Login/{login}")]
    public async Task<JsonResult> Login(string login)
    {
      return await game.LoginAsync(login);
    }

    [HttpGet("Register/{nickname}")]
    public async Task<JsonResult> Register(string nickname)
    {
      return await game.RegisterAsync(nickname);
    }

    [HttpGet("Menu/{token}")]
    public async Task<JsonResult> Menu(string token)
    {
      return await game.MenuAsync(token);
    }
  }
}
