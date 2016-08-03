using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using GhostHuntAR.Infrastructure.Abstract;
using GhostHuntAR.Infrastructure.Models;
using GhostHuntAR.Infrastructure.Models.TransmitModels;
using Supermortal.Common.NonPCL.Helpers;
using Supermortal.Common.NonPCL.Helpers.Log;

namespace GhostHuntAR.Infrastructure.Concrete
{
    public class EFGHLocationRepository : IGHLocationRepository
    {

        private static readonly log4net.ILog Log = LogHelper.GetLogger
                (typeof(EFGHLocationRepository));

        private EFContext db = new EFContext();

        public IQueryable<GHLocation> GetAll()
        {
            try
            {
                return db.GHLocations;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public IQueryable<GHLocation> GetAllWithinDistance(double latitude, double longitude, double maxDistanceKilometers, int maxLocations)
        {
            try
            {
                var sql = Properties.Resources.LocationsByDistance;
                var parameters = new object[] { latitude, longitude, maxDistanceKilometers, maxLocations };

                var list = db.Database.SqlQuery<GHLocation>(sql, parameters.ToArray()).AsQueryable();
                return list;

                //return db.Database.SqlQuery<GHLocation>(sql, parameters.ToArray()).AsQueryable();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public IQueryable<GHLocationStatusTransmitModel> GetAllLocationIdsWithinDistance(double latitude, double longitude, double maxDistanceKilometers,
          int maxLocations)
        {
            try
            {
                var sql = Properties.Resources.LocationStatusesByDistance;
                var parameters = new object[] { latitude, longitude, maxDistanceKilometers, maxLocations };

                var list = db.Database.SqlQuery<GHLocationStatusTransmitModel>(sql, parameters.ToArray()).AsQueryable();
                return list;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public void SearchByKeyword(string keyword, ref Dictionary<long, GHLocation> dict)
        {
            try
            {
                keyword = keyword.ToLower();

                var results = db.GHLocations.Where(l => l.AddressLine.ToLower().Contains(keyword) ||
                    l.Title.ToLower().Contains(keyword)).Distinct();

                foreach (var result in results)
                {
                    if (!dict.ContainsKey(result.GHLocationID))
                        dict.Add(result.GHLocationID, result);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public IQueryable<GHLocationListItemTransmitModel> GetAllUserEditedLocations(string userName)
        {
            try
            {
                var sql = Properties.Resources.GetDistinctLocationIdsSightsAndSounds;
                return db.Database.SqlQuery<GHLocationListItemTransmitModel>(sql, new object[] { userName }).AsQueryable();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public IQueryable<GHLocationListItemTransmitModel> GetAllUserOwnedLocations(long userId)
        {
            try
            {
                return db.GHLocations.Where(l => l.CreatedByUserID == userId).Select(l => new GHLocationListItemTransmitModel(l));
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public IQueryable<GHLocationListItemTransmitModel> GetAllUserOwnedLocations(long userId, int page, int pageSize)
        {
            try
            {
                return db.GHLocations.Where(l => l.CreatedByUserID == userId).OrderBy(l => l.GHLocationID).Skip((page - 1) * pageSize).Take(pageSize).ToList().AsQueryable().Select(l => new GHLocationListItemTransmitModel(l));
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public byte[] GetImage(long id)
        {
            try
            {
                return db.GHLocations.Find(id).Image;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public GHLocation Find(long id)
        {
            try
            {
                return db.GHLocations.Find(id);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public void Insert(GHLocation location)
        {
            try
            {
                db.GHLocations.Add(location);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public void Update(GHLocation location)
        {
            try
            {
                if (CacheHelper<GHLocationTransmitModel>.Initialized &&
                    CacheHelper<GHLocationTransmitModel>.Instance.ContainsKey(location.GHLocationID.ToString()))
                    CacheHelper<GHLocationTransmitModel>.Instance.RemoveCacheable(location.GHLocationID.ToString());

                location.DateLastModified = DateTime.UtcNow;
                db.Entry(location).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public void Delete(GHLocation location)
        {
            try
            {
                db.Entry(location).State = EntityState.Deleted;
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
                db.Entry(db.GHLocations.Find(id)).State = EntityState.Deleted;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public string GetLocationTitle(long id)
        {
            try
            {
                return db.GHLocations.Find(id).Title;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public void SetContext(object context)
        {
            try
            {
                db = (EFContext)context;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public void Dispose()
        {
            try
            {
                db.Dispose();
                db = null;
                db = new EFContext();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public void UpdateDateLastModified(long id)
        {
            try
            {
                Update(Find(id));
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public IQueryable<GHLocationListItemAPITransmitModel> GetAllUserEditedLocationsAPI(string userName)
        {
            try
            {
                var sql = Properties.Resources.GetDistinctLocationIdsSightsAndSounds;
                return db.Database.SqlQuery<GHLocationListItemAPITransmitModel>(sql, new object[] { userName }).AsQueryable();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public IQueryable<GHLocationListItemAPITransmitModel> GetAllUserOwnedLocationsAPI(long userId)
        {
            try
            {
                return db.GHLocations.Where(l => l.CreatedByUserID == userId).Select(l => new GHLocationListItemAPITransmitModel(l));
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

    }
}