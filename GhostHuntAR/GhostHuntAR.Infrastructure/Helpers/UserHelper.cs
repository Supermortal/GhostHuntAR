using System;
using GhostHuntAR.Infrastructure.Abstract;
using Supermortal.Common.NonPCL.Helpers.Log;
using Supermortal.Common.PCL.Helpers;

namespace GhostHuntAR.Infrastructure.Helpers
{
  public static class UserHelper
  {

    private static readonly log4net.ILog Log = LogHelper.GetLogger
            (typeof(UserHelper));

    private static AUserHelper _instance;
    public static AUserHelper Instance {
      get { return _instance ?? (_instance = (AUserHelper)IoCHelper.Instance.GetService(typeof(AUserHelper))); }
    }

    public static void Dispose()
    {
      try
      {
        if (_instance != null)
          _instance.Dispose();

        _instance = null;
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

  }
}