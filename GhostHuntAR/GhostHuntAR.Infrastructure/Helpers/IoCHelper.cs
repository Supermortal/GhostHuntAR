using GhostHuntAR.Infrastructure.Abstract;
using GhostHuntAR.Infrastructure.Concrete;

namespace GhostHuntAR.Infrastructure.Helpers
{
  public static class IoCHelper
  {

    private static AIoCHelper _instance = null;
    public static AIoCHelper Instance
    {
      get { return _instance ?? (_instance = new NinjectIoCHelper()); }
    }

  }
}