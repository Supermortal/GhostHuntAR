using System.Linq;
using System.Web;
using GhostHuntAR.Infrastructure.Models;
using VikingErik.Mvc.ResumingActionResults;

namespace GhostHuntAR.Infrastructure.Abstract
{
  public interface ISoundService
  {
    ResumingFileStreamResult Get(long id);
    Sound GetSound(long id);
    void Save(long ghLocationId, HttpPostedFileBase sound, string soundDescription, string userName);
    void Update(long soundId, HttpPostedFileBase sound, string soundDescription);
    void Delete(long soundId);
    IQueryable<Sound> FindAllByUserName(string userName);
    IQueryable<Sound> FindAllByUserName(string userName, int page, int pageSize);
    IQueryable<Sound> FindAllByGHLocationID(long id);
    IQueryable<Sound> FindAllByGHLocationID(long id, int page, int pageSize);
    void Dispose();
  }
}
