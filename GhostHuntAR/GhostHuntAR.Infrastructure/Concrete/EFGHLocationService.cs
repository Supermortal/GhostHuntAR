using System;
using System.Linq;
using System.Web;
using GhostHuntAR.Infrastructure.Abstract;
using GhostHuntAR.Infrastructure.Helpers;
using GhostHuntAR.Infrastructure.Models;
using GhostHuntAR.Infrastructure.Models.TransmitModels;
using Supermortal.Common.NonPCL.Helpers.Log;

namespace GhostHuntAR.Infrastructure.Concrete
{
    public class EFGHLocationService : IGHLocationService
    {

        private static readonly log4net.ILog Log = LogHelper.GetLogger
            (typeof(EFGHLocationService));

        private readonly IGHLocationRepository _ilr;
        private readonly IGHGeocoderService _gs;

        public EFGHLocationService(IGHLocationRepository ilr, IUserRepository ur, IGHGeocoderService gs)
        {
            _ilr = ilr;
            _gs = gs;

            var db = new EFContext();
            _ilr.SetContext(db);
        }

        public IQueryable<GHLocation> GetAllLocations()
        {
            try
            {
                return _ilr.GetAll();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public IQueryable<GHLocation> GetAllLocationsWithinDistance(double latitude, double longitude, double maxDistanceKilometers,
                                                        int maxLocations)
        {
            try
            {
                return _ilr.GetAllWithinDistance(latitude, longitude, maxDistanceKilometers, maxLocations);
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
                return _ilr.GetAllLocationIdsWithinDistance(latitude, longitude, maxDistanceKilometers, maxLocations);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public GHLocation FindLocation(long id)
        {
            try
            {
                return _ilr.Find(id);
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
                return _ilr.GetImage(id);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public void InsertLocation(GHLocation location)
        {
            try
            {
                //RACE CONDITION
                _gs.ProcessGHLocation(location);
                _ilr.Insert(location);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public bool SaveLocation(GHLocation location, HttpPostedFileBase image, int userId)
        {
            try
            {
                var user = UserHelper.Instance.CurrentUser;
                if (user != null)
                {
                    location.CreatedByUserID = user.UserId;

                    //RACE CONDITION
                    _gs.ProcessGHLocation(location);

                    if (image != null)
                    {
                        var arr = new byte[image.ContentLength];
                        image.InputStream.Read(arr, 0, image.ContentLength);
                        location.Image = arr;
                    }

                    _ilr.Insert(location);

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return false;
            }
        }

        public void UpdateLocation(GHLocation location, HttpPostedFileBase image)
        {
            try
            {
                var l = _ilr.Find(location.GHLocationID);
                if (l == null) return;

                l.SetAllEditedProperties(location);

                if (image != null)
                {
                    var arr = new byte[image.ContentLength];
                    image.InputStream.Read(arr, 0, image.ContentLength);
                    l.Image = arr;
                }

                _ilr.Update(l);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public void DeleteLocation(GHLocation location)
        {
            try
            {
                _ilr.Delete(location);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public void DeleteLocation(long id)
        {
            try
            {
                _ilr.Delete(id);
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
                return _ilr.GetLocationTitle(id);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public IQueryable<GHLocationListItemTransmitModel> GetAllUserEditedLocations(string userName)
        {
            try
            {
                return _ilr.GetAllUserEditedLocations(userName);
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
                return _ilr.GetAllUserOwnedLocations(userId);
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
                return _ilr.GetAllUserOwnedLocations(userId, page, pageSize);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public void Dispose()
        {
            try
            {
                _ilr.Dispose();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public GHLocationTransmitModel GetCacheable(string id)
        {
            return new GHLocationTransmitModel(FindLocation(long.Parse(id)));
        }

    }
}