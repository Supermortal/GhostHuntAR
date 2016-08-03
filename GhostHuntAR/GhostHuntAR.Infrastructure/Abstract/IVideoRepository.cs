using System.Linq;
using GhostHuntAR.Infrastructure.Models;

namespace GhostHuntAR.Infrastructure.Abstract
{
  public interface IVideoRepository
  {
    IQueryable<Video> GetAll();
    Video Find(long id);
    void Insert(Video video);
    void Update(Video video);
    void Delete(Video video);
    void Delete(long id);
    IQueryable<Video> FindAllByUserName(string userName);
    IQueryable<Video> FindAllByUserName(string userName, int page, int pageSize);
    IQueryable<Video> FindAllByGHLocationID(long id);
    IQueryable<Video> FindAllByGHLocationID(long id, int page, int pageSize);
    void Dispose();
  }
}
