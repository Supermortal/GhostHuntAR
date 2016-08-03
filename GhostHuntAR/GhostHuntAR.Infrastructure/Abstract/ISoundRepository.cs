using System.Linq;
using GhostHuntAR.Infrastructure.Models;

namespace GhostHuntAR.Infrastructure.Abstract
{
  public interface ISoundRepository
  {
    IQueryable<Sound> GetAll();
    IQueryable<Sound> FindAllByUserName(string userName);
    IQueryable<Sound> FindAllByUserName(string userName, int page, int pageSize);
    IQueryable<Sound> FindAllByGHLocationID(long id);
    IQueryable<Sound> FindAllByGHLocationID(long id, int page, int pageSize);
    Sound Find(long id);
    void Insert(Sound sound);
    void Update(Sound sound);
    void Delete(Sound sound);
    void Delete(long id);
    void Dispose();
  }
}
