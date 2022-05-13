using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotNet.Client.Extension
{
  public static class Utilities
  {
    public static string GetRandomString(this string value)
    {
      return Path.GetRandomFileName().Replace(".", string.Empty);
    }
  }
}
