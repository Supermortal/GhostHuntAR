using System.Linq;
using GhostHuntAR.Infrastructure.Models;

namespace GhostHuntAR.Infrastructure.Abstract
{
    public interface IRawImageRepository
    {
        IQueryable<RawImage> GetAll();
        RawImage Find(long id);
        string GetContentType(long id);
        void Insert(RawImage rawImage);
        void Update(RawImage rawImage);
        void Delete(RawImage rawImage);
        void Delete(long id);
    }
}
