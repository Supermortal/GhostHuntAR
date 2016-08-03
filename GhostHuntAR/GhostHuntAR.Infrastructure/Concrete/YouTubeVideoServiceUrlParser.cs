using System;
using System.Web;
using GhostHuntAR.Infrastructure.Abstract;
using log4net;
using Supermortal.Common.NonPCL.Helpers.Log;

namespace GhostHuntAR.Infrastructure.Concrete
{
  public class YouTubeVideoServiceUrlParser : IVideoServiceUrlParser
  {

    private static ILog Log = LogHelper.GetLogger(typeof (YouTubeVideoServiceUrlParser));

    public string Parse(string url)
    {
      try
      {
        var parts = url.Split('?');
        var qs = HttpUtility.ParseQueryString("?" + parts[1]);

        return qs["v"];
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

  }
}