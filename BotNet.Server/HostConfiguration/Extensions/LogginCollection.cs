namespace BotNet.Server.HostConfiguration.Extensions
{
  public static class LogginCollection
  {
    public static async Task ConfigureBotnetLoggers(this ILoggingBuilder logger, WebHostBuilderContext context)
    {
      logger.AddConsole();
      logger.SetMinimumLevel(LogLevel.Warning);
    }
  }
}
