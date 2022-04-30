using Microsoft.AspNetCore.Http;

namespace BotNet.Server.HostConfiguration.Extensions
{
  public static class ServicesCollection
  {
    public static async Task ConfigureBotNetServices(this IServiceCollection services, WebHostBuilderContext context)
    {
      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
      services.AddSignalR();
    }
  }
}
