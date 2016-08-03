using System.Linq;
using GhostHuntAR.Infrastructure.Models;

namespace GhostHuntAR.Infrastructure.Abstract
{
  public interface ITextRepository
  {
    IQueryable<Text> GetAll();
    IQueryable<Text> FindAllByUserName(string userName);
    IQueryable<Text> FindAllByUserName(string userName, int page, int pageSize);
    IQueryable<Text> FindAllByGHLocationID(long id);
    IQueryable<Text> FindAllByGHLocationID(long id, int page, int pageSize);
    Text Find(long id);
    void Insert(Text text);
    void Update(Text text);
    void Delete(Text text);
    void Delete(long id);
    void Dispose();
  }
}
