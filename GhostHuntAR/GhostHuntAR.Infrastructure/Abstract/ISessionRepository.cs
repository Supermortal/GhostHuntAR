using System.Linq;
using GhostHuntAR.Infrastructure.Models;

namespace GhostHuntAR.Infrastructure.Abstract
{
    public interface ISessionRepository
    {
        IQueryable<Session> GetAll();
        Session Find(string token);
        Session FindByUserName(string userName);
        void Insert(Session session);
        void Update(Session session);
        void Delete(Session session);
        void Delete(string token);
    }
}
