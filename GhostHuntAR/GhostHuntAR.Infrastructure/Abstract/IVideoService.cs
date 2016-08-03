using System.Linq;
using GhostHuntAR.Infrastructure.Models;

namespace GhostHuntAR.Infrastructure.Abstract
{
  public interface IVideoService
  {
    string Get(long id);
    Video GetVideo(long id);
    void Save(long ghLocationId, string description, string url, string userName, string type);
    void Update(long videoId, string description, string url);
    void Update(long videoId, string description);
    void Delete(long videoId);
    IQueryable<Video> FindAllByUserName(string userName);
    IQueryable<Video> FindAllByUserName(string userName, int page, int pageSize);
    IQueryable<Video> FindAllByGHLocationID(long id);
    IQueryable<Video> FindAllByGHLocationID(long id, int page, int pageSize);
    void Dispose();
  }
}