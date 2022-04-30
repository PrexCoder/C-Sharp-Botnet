using Microsoft.AspNetCore.Hosting;

namespace BotNet.Server.HostConfiguration.Extensions
{
  public static class ConfigurationBuilderExt
  {
    public static async Task ConfigureHostAsync(this IConfigurationBuilder configurationBuilder)
    {
      configurationBuilder.Sources.Clear();
      configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
      configurationBuilder.AddJsonFile("appsettings.json", optional: true);
      configurationBuilder.AddEnvironmentVariables(prefix: "APP_");
    }

    public static async Task ConfigureAppAsync(this IConfigurationBuilder configurationBuilder, WebHostBuilderContext context)
    {
      configurationBuilder.AddJsonFile("Config/appsettings.json", optional: true);
      configurationBuilder.AddJsonFile($"Config/appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true);
      configurationBuilder.AddEnvironmentVariables(prefix: "APP_");
    }
  }
}
