using System;
using System.Web.Mvc;
using GhostHuntAR.Infrastructure.Abstract;
using GhostHuntAR.Infrastructure.Helpers;
using GhostHuntAR.Infrastructure.Models.TransmitModels;
using log4net;

namespace GhostHuntAR.Web.Controllers
{
  [AllowAnonymous]
  public class SearchController : Controller
  {

    private static readonly ILog Log = LogHelper.GetLogger
        (typeof(SearchController));

    private IGHLocationRepository _lr;
    private readonly ISearchService _ss;

    public SearchController(IGHLocationRepository lr, ISearchService ss)
    {
      _lr = lr;
      _ss = ss;
    }

    //
    // GET: /Search/

    public ActionResult Form()
    {
      return View();
    }

    public JsonResult Index(string address, string latitude, string longitude, string keywords, string distanceType, string radius)
    {
      try
      {
        var locations = _ss.Search(address, latitude, longitude, keywords, distanceType,
                                   radius, UserHelper.Instance.CurrentUserId);

        if (locations == null)
          return new JsonResult() { Data = null };

        var ghLocations = locations.Select(l => new GHLocationListItemTransmitModel(l));

        var jr = new JsonResult();
        jr.MaxJsonLength = int.MaxValue;
        jr.Data = ghLocations;

        return jr;
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public ActionResult Results(string address, string city, string state, string zip, string latitude, string longitude, string keywords, string distanceType, string radius)
    {
      try
      {
        var locations = _ss.Search(address, city, state, zip, latitude, longitude, keywords, distanceType,
                                   radius, UserHelper.Instance.CurrentUserId);

        return View(locations);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return HttpNotFound();
      }
    }

  }
}
