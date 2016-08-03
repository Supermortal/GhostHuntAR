using System.Linq;
using System.Web;
using GhostHuntAR.Infrastructure.Models;
using GhostHuntAR.Infrastructure.Models.TransmitModels;
using Supermortal.Common.PCL.Abstract;

namespace GhostHuntAR.Infrastructure.Abstract
{
  public interface IGHLocationService : ICacheableService<GHLocationTransmitModel>
  {
    IQueryable<GHLocation> GetAllLocations();
    IQueryable<GHLocation> GetAllLocationsWithinDistance(double latitude, double longitude, double maxDistanceKilometers, int maxLocations);
    IQueryable<GHLocationStatusTransmitModel> GetAllLocationIdsWithinDistance(double latitude, double longitude, double maxDistanceKilometers, int maxLocations);
    GHLocation FindLocation(long id);
    byte[] GetImage(long id);
    void InsertLocation(GHLocation location);
    bool SaveLocation(GHLocation location, HttpPostedFileBase image, int userId);
    void UpdateLocation(GHLocation location, HttpPostedFileBase image);
    void DeleteLocation(GHLocation location);
    void DeleteLocation(long id);
    string GetLocationTitle(long id);
    IQueryable<GHLocationListItemTransmitModel> GetAllUserEditedLocations(string userName);
    IQueryable<GHLocationListItemTransmitModel> GetAllUserOwnedLocations(long userId);
    IQueryable<GHLocationListItemTransmitModel> GetAllUserOwnedLocations(long userId, int page, int pageSize);
    void Dispose();
  }
}
