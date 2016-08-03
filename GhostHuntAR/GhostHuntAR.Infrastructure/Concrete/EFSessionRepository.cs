using System;
using System.Data;
using System.Linq;
using GhostHuntAR.Infrastructure.Abstract;
using GhostHuntAR.Infrastructure.Models;
using Supermortal.Common.NonPCL.Helpers.Log;

namespace GhostHuntAR.Infrastructure.Concrete
{
    public class EFSessionRepository : ISessionRepository 
    {

        private static readonly log4net.ILog Log = LogHelper.GetLogger
            (typeof(EFSessionRepository));

        private EFContext db = new EFContext();

        public IQueryable<Session> GetAll()
        {
            try
            {
                return db.Sessions;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public Session Find(string token)
        {
            try
            {
                return db.Sessions.Find(token);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public Session FindByUserName(string userName)
        {
            try
            {
                return db.Sessions.SingleOrDefault(s => s.UserName == userName);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public void Insert(Session session)
        {
            try
            {
                db.Sessions.Add(session);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public void Update(Session session)
        {
            try
            {
                db.Entry(session).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public void Delete(Session session)
        {
            try
            {
                db.Entry(session).State = EntityState.Deleted;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public void Delete(string token)
        {
            try
            {
                db.Entry(db.Sessions.Find(token)).State = EntityState.Deleted;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

    }
}