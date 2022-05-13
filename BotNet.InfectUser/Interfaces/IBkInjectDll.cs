using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotNet.InfectUser.Interfaces
{
  public interface IBkInjectDll
  {
    Task InjectSvcAsync();
  }
}
