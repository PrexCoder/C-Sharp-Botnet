using Microsoft.AspNetCore.SignalR.Client;

namespace BotNet.Client.Connection.Extensions
{
  public static class ConnectionEventsConfiguration
  {
    public static async Task ConfigureBotNetEventsAsync(this HubConnection hub)
    {
      hub.On("test", () =>
      {
      });
    }
  }
}
