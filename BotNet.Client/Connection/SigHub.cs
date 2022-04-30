using BotNet.Client.Connection.Extensions;
using BotNet.Client.Connection.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;

namespace BotNet.Client.Connection
{
  public class SigHub : ISigHub
  {
    public HubConnection? Connection { get; set; }

    public async Task StartConnectionAsync(string? hubUrl = "http://localhost/connection")
    {
      if(hubUrl == null)
      {
        return;
      }

      Connection = new HubConnectionBuilder().WithUrl(hubUrl).WithAutomaticReconnect().Build();

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
