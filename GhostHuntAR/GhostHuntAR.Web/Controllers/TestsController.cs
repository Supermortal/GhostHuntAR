using System;
using System.Web.Mvc;
using GhostHuntAR.Infrastructure.Abstract;
using log4net;

namespace GhostHuntAR.Web.Controllers
{
  [Authorize(Roles = "admin")]
  public class TestsController : Controller
  {

    private static readonly ILog Log = LogHelper.GetLogger
            (typeof(TestsController));

    private readonly ITestExceptionService _tes;

    public TestsController(ITestExceptionService tes)
    {
      _tes = tes;
    }

    public ActionResult LogStatus()
    {
      ViewBag.Configured = LogHelper.Configured;
      ViewBag.ConfigFilePath = LogHelper.ConfigFilePath;
      ViewBag.TriedToConfigure = LogHelper.TriedToConfigure;

      return View();
    }

    public ActionResult ThrowWebException()
    {
      try
      {
        throw new Exception("Web Exception");
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return View();
      }
    }

    public ActionResult ThrowInfrastructureException()
    {
      try
      {
        _tes.ThrowException();

        return View();
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return HttpNotFound();
      }
    }

  }
}
