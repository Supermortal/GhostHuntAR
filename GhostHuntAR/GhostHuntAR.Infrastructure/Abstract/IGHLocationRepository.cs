using System.Collections.Generic;
using System.Linq;
using GhostHuntAR.Infrastructure.Models;
using GhostHuntAR.Infrastructure.Models.TransmitModels;

namespace GhostHuntAR.Infrastructure.Abstract
{
  public interface IGHLocationRepository
  {
    IQueryable<GHLocation> GetAll();
    IQueryable<GHLocation> GetAllWithinDistance(double latitude, double longitude, double maxDistanceKilometers, int maxLocations);
    IQueryable<GHLocationStatusTransmitModel> GetAllLocationIdsWithinDistance(double latitude, double longitude, double maxDistanceKilometers, int maxLocations);
    void SearchByKeyword(string keyword, ref Dictionary<long, GHLocation> dict);
    IQueryable<GHLocationListItemTransmitModel> GetAllUserEditedLocations(string userName);
    IQueryable<GHLocationListItemAPITransmitModel> GetAllUserEditedLocationsAPI(string userName);
    IQueryable<GHLocationListItemAPITransmitModel> GetAllUserOwnedLocationsAPI(long userId);
    IQueryable<GHLocationListItemTransmitModel> GetAllUserOwnedLocations(long userId);
    IQueryable<GHLocationListItemTransmitModel> GetAllUserOwnedLocations(long userId, int page, int pageSize);
    byte[] GetImage(long id);
    GHLocation Find(long id);
    void Insert(GHLocation location);
    void Update(GHLocation location);
    void Delete(GHLocation location);
    void Delete(long id);
    string GetLocationTitle(long id);
    void SetContext(object context);
    void Dispose();
    void UpdateDateLastModified(long id);
  }
}
