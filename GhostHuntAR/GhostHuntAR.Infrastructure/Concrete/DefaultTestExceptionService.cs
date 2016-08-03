using System;
using GhostHuntAR.Infrastructure.Abstract;
using Supermortal.Common.NonPCL.Helpers.Log;

namespace GhostHuntAR.Infrastructure.Concrete
{
  public class DefaultTestExceptionService : ITestExceptionService
  {

    private static readonly log4net.ILog Log = LogHelper.GetLogger
        (typeof(DefaultTestExceptionService));

    public void ThrowException()
    {
      try
      {
        throw new Exception("Infrastructure Exception");
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

  }
}