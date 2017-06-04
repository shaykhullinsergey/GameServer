using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grey.sh_Server.Components
{
  public class Battle
  {
    public Matchmaker Matchmaker { get; set; }

    public async Task<JsonResult> EndTurnAsync()
    {
      return null;
    }
  }
}
