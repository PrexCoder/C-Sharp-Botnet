using BotNet.Client.Connection;

namespace Botnet.Client
{
  public class Program
  {
    public async Task Initialize()
    {
      var sigHub = new SigHub();
      await sigHub.StartConnection();
    }
  }
}

