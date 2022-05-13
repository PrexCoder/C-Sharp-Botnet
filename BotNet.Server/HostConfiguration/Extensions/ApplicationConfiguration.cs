

namespace BotNet.Server.HostConfiguration.Extensions
{
  public static class ApplicationConfiguration
  {
    public static async Task ConfigureBotnetAppAsync(this IApplicationBuilder app)
    {
      app.UseStaticFiles();

      app.UseRouting();

      app.UseHttpLogging();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapHub<ConnectionHub>("connection");
      });

      app.Run(async (context) => await context.Response.WriteAsync("Bot App Started"));
    }
  }
}
