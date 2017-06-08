using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using GameServer.Repositories;
using GameServer.Components;

namespace GameServer
{
  public class Startup
  {
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc();
      services.AddSingleton<Game>();
      services.AddSingleton<Battle>();

      services.AddSingleton<PlayerContext>();
      services.AddSingleton<PlayerRepository>();
      services.AddSingleton<GameRepository>();

      services.AddSingleton<Matchmaker>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, ILoggerFactory logger)
    {
      logger.AddConsole();

      app.UseMvc();
    }
  }
}
