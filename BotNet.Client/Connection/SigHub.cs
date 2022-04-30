using BotNet.Client.Connection.Extensions;
using Microsoft.AspNetCore.SignalR.Client;

#nullable disable
namespace BotNet.Client.Connection
{
  public class SigHub
  {
    public HubConnection Connection { get; set; }

    public async Task StartConnection()
    {
      Connection = new HubConnectionBuilder().WithUrl("http://localhost/connection").WithAutomaticReconnect().Build();

      await Connection.ConfigureBotNetEventsAsync();

      Connection.Closed += async (error) =>
      {
        await Task.Delay(new Random().Next(0, 5) * 1000);
        await Connection.StartAsync();
      };

      await Connection.StartAsync();
    }
  }
}
