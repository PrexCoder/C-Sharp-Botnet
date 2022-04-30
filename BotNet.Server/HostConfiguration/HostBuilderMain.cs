namespace BotNet.Server.HostConfiguration
{
  public class HostBuilderMain
  {
    /// <summary>
    /// Hosted Services
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public async Task CreateHost()
    {
      await WebHost.CreateDefaultBuilder()
        .UseSetting(WebHostDefaults.DetailedErrorsKey, "true")
        .UseEnvironment("Development")
        .UseWebRoot("public")
        .CaptureStartupErrors(true)
        .ConfigureAppConfiguration(async (context, configApp) => await configApp.ConfigureAppAsync(context))
        .ConfigureServices(async (context, services) => await services.ConfigureBotNetServices(context))
        .ConfigureLogging(async (context, configLogging) => await configLogging.ConfigureBotnetLoggers(context))
        .ConfigureKestrel(async (context, options) => await options.ConfigureKestralAsync(context))
        .Configure(async (app) => await app.ConfigureBotnetAppAsync())
        .Build().RunAsync();
    }
  }
}


