using System;
using System.Data;
using System.Linq;
using GhostHuntAR.Infrastructure.Abstract;
using GhostHuntAR.Infrastructure.Models;
using Supermortal.Common.NonPCL.Helpers.Log;

namespace GhostHuntAR.Infrastructure.Concrete
{
    public class EFRawImageRepository : IRawImageRepository 
    {

        private static readonly log4net.ILog Log = LogHelper.GetLogger
            (typeof(EFRawImageRepository));

        private EFContext db = new EFContext();

        public IQueryable<RawImage> GetAll()
        {
            try
            {
                return db.RawImages;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public RawImage Find(long id)
        {
            try
            {
                return db.RawImages.Find(id);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public string GetContentType(long id)
        {
            try
            {
                return db.RawImages.Find(id).MIMEType;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public void Insert(RawImage rawImage)
        {
            try
            {
                db.RawImages.Add(rawImage);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public void Update(RawImage rawImage)
        {
            try
            {
                db.Entry(rawImage).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public void Delete(RawImage rawImage)
        {
            try
            {
                db.Entry(rawImage).State = EntityState.Deleted;
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
                db.Entry(db.RawImages.Find(id)).State = EntityState.Deleted;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }
    }
}