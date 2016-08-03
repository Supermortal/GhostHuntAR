using System.Linq;
using System.Web;
using System.Web.Mvc;
using GhostHuntAR.Infrastructure.Models;

namespace GhostHuntAR.Infrastructure.Abstract
{
  public interface IImageService
  {
    string GetContentType(long id);
    FileResult Get(long id);
    byte[] GetBytes(long id);
    Image GetImage(long id);
    void Save(long ghLocationId, HttpPostedFileBase image, string caption, string userName);
    void Update(long imageId, HttpPostedFileBase image, string caption);
    void Delete(long imageId);
    IQueryable<Image> FindAllByUserName(string userName);
    IQueryable<Image> FindAllByUserName(string userName, int page, int pageSize);
    IQueryable<Image> FindAllByGHLocationID(long id);
    IQueryable<Image> FindAllByGHLocationID(long id, int page, int pageSize);
    void Dispose();
  }
}
