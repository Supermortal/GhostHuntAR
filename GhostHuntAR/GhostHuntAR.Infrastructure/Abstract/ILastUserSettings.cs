using System.Linq;
using GhostHuntAR.Infrastructure.Models;

namespace GhostHuntAR.Infrastructure.Abstract
{
  public interface ILastUserSettings
  {
    IQueryable<LastUserSettings> GetAll();
    IQueryable<LastUserSettings> FindAllByUserId(long userId);
    LastUserSettings Find(string id);
    void Insert(LastUserSettings lastUserSettings);
    void Update(LastUserSettings lastUserSettings);
    void Delete(LastUserSettings lastUserSettings);
    void Delete(string id);
    void Dispose();
  }
}
