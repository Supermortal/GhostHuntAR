using System.Linq;
using GhostHuntAR.Infrastructure.Models;

namespace GhostHuntAR.Infrastructure.Abstract
{
  public interface ITextService
  {
    string Get(long id);
    Text GetText(long id);
    void Save(long ghLocationId, string title, string body, string userName);
    void Update(long textId, string title, string body);
    void Update(long textId, string title);
    void Delete(long textId);
    IQueryable<Text> FindAllByUserName(string userName);
    IQueryable<Text> FindAllByUserName(string userName, int page, int pageSize);
    IQueryable<Text> FindAllByGHLocationID(long id);
    IQueryable<Text> FindAllByGHLocationID(long id, int page, int pageSize);
    void Dispose();
  }
}
