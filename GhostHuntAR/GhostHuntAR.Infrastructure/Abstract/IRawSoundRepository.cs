using System.Linq;
using GhostHuntAR.Infrastructure.Models;

namespace GhostHuntAR.Infrastructure.Abstract
{
    public interface IRawSoundRepository
    {
        IQueryable<RawSound> GetAll();
        RawSound Find(long id);
        void Insert(RawSound rawSound);
        void Update(RawSound rawSound);
        void Delete(RawSound rawSound);
        void Delete(long id);
    }
}
