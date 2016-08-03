using System.Linq;
using GhostHuntAR.Infrastructure.Models;

namespace GhostHuntAR.Infrastructure.Abstract
{
    public interface IRawTextRepository
    {
        IQueryable<RawText> GetAll();
        RawText Find(long id);
        void Insert(RawText rawText);
        void Update(RawText rawText);
        void Delete(RawText rawText);
        void Delete(long id);
    }
}
