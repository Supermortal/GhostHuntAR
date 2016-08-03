using System.Collections.Generic;
using System.Threading.Tasks;
using GhostHuntAR.Infrastructure.Models;

namespace GhostHuntAR.Infrastructure.Abstract
{
  public interface ISearchService
  {
    Task<IEnumerable<GHLocation>> FromSavedSearch(SavedSearch savedSearch);
    Task<IEnumerable<GHLocation>> Search(string address, string city, string state, string zip, string latitude, string longitude, string keywords, string distanceType, string radius, int? userId);
    Task<IEnumerable<GHLocation>> Search(string address, string latitude, string longitude, string keywords, string distanceType, string radius, int? userId);
  }
}
