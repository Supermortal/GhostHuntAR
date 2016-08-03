using System;
using System.IO;
using System.Text;
using log4net;

namespace GhostHuntAR.Infrastructure.Helpers
{
  public static class LogHelper
  {

    private static volatile bool _configured = false;
    private static readonly object Lock = new object();

    public static ILog GetLogger(Type type)
    {
      try
      {
        if (!_configured)
        {
          //reduce lock overhead with 2 checks of _configured
          lock (Lock)
          {
            if (!_configured)
            {
              var appDomain = AppDomain.CurrentDomain.BaseDirectory;
              var parts = appDomain.Split('\\');

              var sb = new StringBuilder();
              for (var i = 0; i < parts.Length - 2; i++)
              {
                sb.Append(parts[i] + "\\");
              }
              var infrastructureDir = sb.ToString() + "GhostHuntAR.Infrastructure";


              log4net.Config.XmlConfigurator.Configure(
                new FileInfo(
                  Path.Combine(infrastructureDir, "log4net.config")));

              _configured = true;
            }
          }
        }

        return LogManager.GetLogger(type);
      }
      catch (Exception ex)
      {
        _configured = false;
        return null;
      }
    }

  }
}