using grey.sh_Server.Contexts;
using grey.sh_Server.Components;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;


namespace grey.sh_Server
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc();
      services.AddSingleton<Game>();
      services.AddSingleton<Battle>();
      services.AddSingleton<Matchmaker>();
      services.AddSingleton<GameRepository>();
      services.AddSingleton<PlayerRepository>();
      services.AddDbContext<PlayerContext>();
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      loggerFactory.AddConsole();
      app.UseMvc();
    }
  }
}
