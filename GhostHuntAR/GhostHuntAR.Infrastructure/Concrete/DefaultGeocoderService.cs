using System;
using System.Threading.Tasks;
using GhostHuntAR.Infrastructure.Abstract;
using GhostHuntAR.Infrastructure.Models;
using Supermortal.Common.NonPCL.Helpers.Log;
using Supermortal.Common.PCL.Abstract;
using Supermortal.Common.PCL.Models;

namespace GhostHuntAR.Infrastructure.Concrete
{
  public class DefaultGeocoderService : IGHGeocoderService
  {

    private static readonly log4net.ILog Log = LogHelper.GetLogger
        (typeof(DefaultGeocoderService));

    private readonly IGeocoderService _gs;

    public DefaultGeocoderService(IGeocoderService gs)
    {
      _gs = gs;
    }

    public async Task<GeoCoord> Lookup(string address)
    {
      try
      {
        return await _gs.Lookup(address);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public async Task<GeoCoord> ReverseLookup(double latitude, double longitude)
    {
      try
      {
        return await _gs.ReverseLookup(latitude, longitude);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public async Task ProcessGHLocation(GHLocation location)
    {
      try
      {
        var gc = new GeoCoord();

        if (location.Latitude != null && location.Longitude != null)
        {
          gc = await ReverseLookup((double)location.Latitude, (double)location.Longitude);
          location.AddressLine = gc.StreetNumber + " " + gc.Route + ((string.IsNullOrEmpty(gc.UnitNumber)) ? string.Empty : " #" + gc.UnitNumber);
          location.City = gc.City;
          location.State = gc.State;
          location.ZipPostalCode = gc.ZipPostalCode;
        }
        else
        {
          gc = await Lookup(location.AddressLine + ", " + location.City + ", " + location.State + " " + location.ZipPostalCode);
          location.Latitude = gc.Latitude;
          location.Longitude = gc.Longitude;
        }
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

  }
}