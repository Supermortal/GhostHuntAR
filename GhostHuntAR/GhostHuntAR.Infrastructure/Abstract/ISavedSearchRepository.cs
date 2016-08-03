using System.Linq;
using GhostHuntAR.Infrastructure.Models;

namespace GhostHuntAR.Infrastructure.Abstract
{
  public interface ISavedSearchRepository
  {
    IQueryable<SavedSearch> GetAll();
    SavedSearch Find(string id);
    void Insert(SavedSearch savedSearch);
    void Update(SavedSearch savedSearch);
    void Delete(SavedSearch savedSearch);
    void Delete(string id);
    void DeleteByUserId(long userId);
    void Dispose();
    void SetContext(object context);
  }
}
