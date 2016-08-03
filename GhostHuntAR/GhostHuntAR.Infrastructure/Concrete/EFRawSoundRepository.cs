using System;
using System.Data;
using System.Linq;
using GhostHuntAR.Infrastructure.Abstract;
using GhostHuntAR.Infrastructure.Models;
using Supermortal.Common.NonPCL.Helpers.Log;

namespace GhostHuntAR.Infrastructure.Concrete
{
    public class EFRawSoundRepository : IRawSoundRepository 
    {

        private static readonly log4net.ILog Log = LogHelper.GetLogger
            (typeof(EFRawSoundRepository));

        private EFContext db = new EFContext();

        public IQueryable<RawSound> GetAll()
        {
            try
            {
                return db.RawSounds;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public RawSound Find(long id)
        {
            try
            {
                return db.RawSounds.Find(id);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public void Insert(RawSound rawSound)
        {
            try
            {
                db.RawSounds.Add(rawSound);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public void Update(RawSound rawSound)
        {
            try
            {
                db.Entry(rawSound).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public void Delete(RawSound rawSound)
        {
            try
            {
                db.Entry(rawSound).State = EntityState.Deleted;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public void Delete(long id)
        {
            try
            {
                db.Entry(db.RawSounds.Find(id)).State = EntityState.Deleted;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }
    }
}