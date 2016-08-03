using System;
using System.Data;
using System.Linq;
using GhostHuntAR.Infrastructure.Abstract;
using GhostHuntAR.Infrastructure.Models;
using GhostHuntAR.Infrastructure.Models.TokenRegister;
using Supermortal.Common.NonPCL.Helpers.Log;

namespace GhostHuntAR.Infrastructure.Concrete
{
    public class EFPotentialUserRepository : IPotentialUserRepository
    {

        private static readonly log4net.ILog Log = LogHelper.GetLogger
            (typeof(EFPotentialUserRepository));

        private EFContext db = new EFContext();

        public void GetAllWithCount(ref PotentialUserParcel pup, int page, int pageSize, bool? signedUp)
        {
            try
            {
                var potentialUsers = (IQueryable<PotentialUser>)db.PotentialUsers;
                var count = potentialUsers.Count();

                if (signedUp == null)
                    potentialUsers = potentialUsers.OrderBy(p => p.Name).Skip((page - 1) * pageSize).Take(pageSize);
                else
                    potentialUsers = potentialUsers.Where(p => p.SignedUp == signedUp).OrderBy(p => p.Name).Skip((page - 1) * pageSize).Take(pageSize);

                pup.Count = count;
                pup.PotentialUsers = potentialUsers;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public PotentialUser Find(long id)
        {
            try
            {
                return db.PotentialUsers.Find(id);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public PotentialUser FindByToken(string token)
        {
            try
            {
                return db.PotentialUsers.SingleOrDefault(pu => pu.Token == token);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public void Save(PotentialUser pu)
        {
            try
            {
                db.PotentialUsers.Add(pu);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public void Update(PotentialUser pu)
        {
            try
            {
                db.Entry(pu).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public void Delete(PotentialUser pu)
        {
            try
            {
                db.Entry(pu).State = EntityState.Deleted;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public void Delete(long id)
        {
            try
            {
                db.Entry(db.PotentialUsers.Find(id)).State = EntityState.Deleted;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw ex;
            }
        }
    }
}