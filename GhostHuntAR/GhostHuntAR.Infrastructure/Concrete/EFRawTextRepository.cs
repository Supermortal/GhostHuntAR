using System;
using System.Data;
using System.Linq;
using GhostHuntAR.Infrastructure.Abstract;
using GhostHuntAR.Infrastructure.Models;
using Supermortal.Common.NonPCL.Helpers.Log;

namespace GhostHuntAR.Infrastructure.Concrete
{
    public class EFRawTextRepository : IRawTextRepository 
    {

        private static readonly log4net.ILog Log = LogHelper.GetLogger
            (typeof(EFRawTextRepository));

        private EFContext db = new EFContext();

        public IQueryable<RawText> GetAll()
        {
            try
            {
                return db.RawTexts;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public RawText Find(long id)
        {
            try
            {
                return db.RawTexts.Find(id);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public void Insert(RawText rawText)
        {
            try
            {
                db.RawTexts.Add(rawText);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public void Update(RawText rawText)
        {
            try
            {
                db.Entry(rawText).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public void Delete(RawText rawText)
        {
            try
            {
                db.Entry(rawText).State = EntityState.Deleted;
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
                db.Entry(db.RawTexts.Find(id)).State = EntityState.Deleted;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }
    }
}