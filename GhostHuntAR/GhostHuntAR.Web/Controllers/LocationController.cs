using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using GhostHuntAR.Infrastructure.Abstract;
using GhostHuntAR.Infrastructure.Models.TransmitModels;
using GhostHuntAR.Web.Filters;
using log4net;
using Supermortal.Common.NonPCL.Helpers;
using Supermortal.Common.NonPCL.Helpers.Log;
using Supermortal.Common.PCL.Helpers;
using Supermortal.Common.PCL.Models;

namespace GhostHuntAR.Web.Controllers
{
  [TokenFilter]
  public class LocationController : ApiController
  {

    private static readonly ILog Log = LogHelper.GetLogger
        (typeof(LocationController));

    private readonly IGHLocationRepository db = (IGHLocationRepository)IoCHelper.Instance.GetService(typeof(IGHLocationRepository));
    private readonly ISessionService _ss = (ISessionService) IoCHelper.Instance.GetService(typeof (ISessionService));

    // GET api/location
    [HttpGet]
    public ServerReturnModel<IEnumerable<GHLocationStatusTransmitModel>> GetLocationIds(double latitude, double longitude, double maxDistanceKilometers, int maxLocations)
    {
      try
      {
        return new ServerReturnModel<IEnumerable<GHLocationStatusTransmitModel>>("OK", db.GetAllLocationIdsWithinDistance(latitude, longitude, maxDistanceKilometers, maxLocations));
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return new ServerReturnModel<IEnumerable<GHLocationStatusTransmitModel>>("ERROR", ex.Message);
      }
    }

    // GET api/location/5
    [HttpGet]
    public ServerReturnModel<GHLocationTransmitModel> GetLocation(long id)
    {
      try
      {
        return new ServerReturnModel<GHLocationTransmitModel>("OK", CacheHelper<GHLocationTransmitModel>.Instance.GetCacheable(id.ToString()));
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return new ServerReturnModel<GHLocationTransmitModel>("ERROR", ex.Message);
      }
    }

    //// POST api/location
    //[HttpPost]
    //public void PostLocation([FromBody]GHLocation location)
    //{
    //  try
    //  {
    //    db.Insert(location);
    //  }
    //  catch (Exception ex)
    //  {
    //    Log.Error(ex.Message, ex);
    //  }
    //}

    //// PUT api/location/5
    //[HttpPost]
    //public void PutLocation([FromBody]GHLocation location)
    //{
    //  try
    //  {
    //    db.Update(location);
    //  }
    //  catch (Exception ex)
    //  {
    //    Log.Error(ex.Message, ex);
    //  }
    //}

    [HttpPost]
    public ServerReturnModel<IEnumerable<GHLocationListItemAPITransmitModel>> GetUserOwnedEditedLocations()
    {
      try
      {
        var token = Request.Headers.SingleOrDefault(h => h.Key == "GH-AUTH-TOKEN").Value.FirstOrDefault();
        var user = _ss.GetUser(token);
        return new ServerReturnModel<IEnumerable<GHLocationListItemAPITransmitModel>>("OK", db.GetAllUserEditedLocationsAPI((user.UserName)).ToList());
      }
      catch (Exception ex)
      {
        return new ServerReturnModel<IEnumerable<GHLocationListItemAPITransmitModel>>("ERROR", ex.Message);
      }
    }

    [HttpPost]
    public ServerReturnModel<IEnumerable<GHLocationListItemAPITransmitModel>> GetUserOwnedLocations()
    {
      try
      {
        var token = Request.Headers.SingleOrDefault(h => h.Key == "GH-AUTH-TOKEN").Value.FirstOrDefault();
        var user = _ss.GetUser(token);
        return new ServerReturnModel<IEnumerable<GHLocationListItemAPITransmitModel>>("OK", db.GetAllUserOwnedLocationsAPI(user.UserId).ToList());
      }
      catch (Exception ex)
      {
        return new ServerReturnModel<IEnumerable<GHLocationListItemAPITransmitModel>>("ERROR", ex.Message);
      }
    }

    [HttpPost]
    public ServerReturnModel<object> GetLocationImage(long id)
    {
      try
      {
        var image = Convert.ToBase64String(db.GetImage(id));
        return new ServerReturnModel<object>("OK", (object)image);
      }
      catch (Exception ex)
      {
        return new ServerReturnModel<object>("ERROR", ex.Message);
      }
    } 
 
    // DELETE api/location/5
    //[HttpPost]
    //public void DeleteLocation(int id)
    //{
    //  try
    //  {
    //    db.Delete(id);
    //  }
    //  catch (Exception ex)
    //  {
    //    Log.Error(ex.Message, ex);
    //  }
    //}

  }
}
