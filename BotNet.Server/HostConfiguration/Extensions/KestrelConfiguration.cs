using Microsoft.AspNetCore.Server.Kestrel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotNet.Server.HostConfiguration.Extensions
{
  public static class KestrelConfiguration
  {
    public static async Task ConfigureKestralAsync(this KestrelServerOptions kestralOptions, WebHostBuilderContext context)
    {
      kestralOptions.Limits.MaxRequestBodySize = 20000000;
      kestralOptions.ListenAnyIP(80);
    }
  }
}
