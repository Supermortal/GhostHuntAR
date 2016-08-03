using System.Linq;
using GhostHuntAR.Infrastructure.Models;

namespace GhostHuntAR.Infrastructure.Abstract
{
  public interface IImageRepository
  {
    IQueryable<Image> GetAll();
    IQueryable<Image> FindAllByUserName(string userName);
    IQueryable<Image> FindAllByUserName(string userName, int page, int pageSize);
    IQueryable<Image> FindAllByGHLocationID(long id);
    IQueryable<Image> FindAllByGHLocationID(long id, int page, int pageSize);
    Image Find(long id);
    void Insert(Image image);
    void Update(Image image);
    void Delete(Image image);
    void Delete(long id);
    void Dispose();
  }
}
