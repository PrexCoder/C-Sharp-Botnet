using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotNet.Client.Connection.Interfaces
{
  public interface ISigHub
  {
    Task StartConnectionAsync(string? hubUrl = "http://localhost/connection");
  }
}
