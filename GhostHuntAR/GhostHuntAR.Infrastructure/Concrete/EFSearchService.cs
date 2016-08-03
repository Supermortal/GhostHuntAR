using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GhostHuntAR.Infrastructure.Abstract;
using GhostHuntAR.Infrastructure.Helpers;
using GhostHuntAR.Infrastructure.Models;
using GhostHuntAR.Infrastructure.Models.Enums;
using Supermortal.Common.NonPCL.Helpers.Log;
using Supermortal.Common.PCL.Helpers;

namespace GhostHuntAR.Infrastructure.Concrete
{
    public class EFSearchService : ISearchService
    {

        private static readonly log4net.ILog Log = LogHelper.GetLogger
            (typeof(EFSearchService));

        private readonly IGHLocationRepository _lr;
        private readonly IGHGeocoderService _gs;
        private readonly IUserRepository _ur;

        public EFSearchService(IGHLocationRepository lr, IGHGeocoderService gs, IUserRepository ur)
        {
            _lr = lr;
            _gs = gs;
            _ur = ur;
        }

        public async Task<IEnumerable<GHLocation>> FromSavedSearch(SavedSearch savedSearch)
        {
            try
            {
                if (string.IsNullOrEmpty(savedSearch.City) && string.IsNullOrEmpty(savedSearch.State) &&
                    string.IsNullOrEmpty(savedSearch.Zip))
                {
                    return await Search(savedSearch.Address, savedSearch.Latitude, savedSearch.Longitude, savedSearch.Keywords,
                      savedSearch.DistanceType, savedSearch.Radius, null);
                }
                else
                {
                    return await Search(savedSearch.Address, savedSearch.City, savedSearch.State, savedSearch.Zip, savedSearch.Latitude, savedSearch.Longitude, savedSearch.Keywords,
                      savedSearch.DistanceType, savedSearch.Radius, null);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public async Task<IEnumerable<GHLocation>> Search(string address, string city, string state, string zip, string latitude, string longitude, string keywords, string distanceType, string radius, int? userId)
        {
            try
            {
                IEnumerable<GHLocation> locations = new List<GHLocation>();
                if (!string.IsNullOrEmpty(keywords))
                {
                    locations = SearchByKeywords(keywords);
                }
                else if (!string.IsNullOrEmpty(latitude) && !string.IsNullOrEmpty(longitude))
                {
                    locations = SearchByLatLong(double.Parse(latitude), double.Parse(longitude), double.Parse(radius),
                                                distanceType);
                }
                else
                {
                    locations = await SearchByAddress(address, city, state, zip, distanceType, radius);
                }

                if (userId != null && locations != null && locations.Any())
                {
                    var user = _ur.Find((long)userId);

                    if (user != null)
                    {

                        var savedSearch = new SavedSearch()
                        {
                            Address = address,
                            DistanceType = distanceType,
                            Keywords = keywords,
                            Latitude = latitude,
                            Longitude = longitude,
                            Radius = radius,
                            UserId = (int)userId
                        };
                        _ur.SaveSearch((long)userId, savedSearch);
                        UserHelper.Dispose();

                    }

                }

                return locations;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public async Task<IEnumerable<GHLocation>> Search(string address, string latitude, string longitude, string keywords, string distanceType,
                                  string radius, int? userId)
        {
            try
            {
                IEnumerable<GHLocation> locations = new List<GHLocation>();
                if (!string.IsNullOrEmpty(keywords))
                {
                    locations = SearchByKeywords(keywords);
                }
                else if (!string.IsNullOrEmpty(latitude) && !string.IsNullOrEmpty(longitude))
                {
                    locations = SearchByLatLong(double.Parse(latitude), double.Parse(longitude), double.Parse(radius),
                                                distanceType);
                }
                else
                {
                    locations = await SearchByAddress(address, distanceType, radius);
                }

                if (userId != null && locations != null && locations.Any())
                {
                    var user = _ur.Find((int)userId);

                    if (user != null)
                    {

                        var savedSearch = new SavedSearch()
                        {
                            Address = address,
                            DistanceType = distanceType,
                            Keywords = keywords,
                            Latitude = latitude,
                            Longitude = longitude,
                            Radius = radius,
                            UserId = (int)userId
                        };
                        _ur.SaveSearch((long)userId, savedSearch);
                        UserHelper.Dispose();

                    }

                }

                return locations;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        private IEnumerable<GHLocation> SearchByKeywords(string keywords)
        {
            try
            {
                var regEx = new Regex(@"\w+\b");
                var matches = regEx.Matches(keywords);

                var dict = new Dictionary<long, GHLocation>();
                foreach (Match match in matches)
                {
                    _lr.SearchByKeyword(match.Value, ref dict);
                }

                return dict.Values.ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        private IEnumerable<GHLocation> SearchByLatLong(double latitude, double longitude, double radius, string distanceType)
        {
            try
            {
                var kilometers = radius;
                if (distanceType == DistanceTypes.Miles.ToString())
                    kilometers = ConversionHelper.MilesToKilometers(radius);

                var locations = _lr.GetAllWithinDistance(latitude, longitude, kilometers, 100);

                return locations;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        private async Task<IEnumerable<GHLocation>> SearchByAddress(string address, string city, string state, string zip, string distanceType, string radius)
        {
            try
            {
                var kilometers = double.Parse(radius);
                if (distanceType == DistanceTypes.Miles.ToString())
                    kilometers = ConversionHelper.MilesToKilometers(kilometers);

                var gc = await _gs.Lookup(address + " " + city + ", " + state + " " + zip);

                var locations = _lr.GetAllWithinDistance(gc.Latitude, gc.Longitude, kilometers, 100);

                return locations;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        private async Task<IEnumerable<GHLocation>> SearchByAddress(string address, string distanceType, string radius)
        {
            try
            {
                var kilometers = double.Parse(radius);
                if (distanceType == DistanceTypes.Miles.ToString())
                    kilometers = ConversionHelper.MilesToKilometers(kilometers);

                var gc = await _gs.Lookup(address);

                var locations = _lr.GetAllWithinDistance(gc.Latitude, gc.Longitude, kilometers, 100);

                return locations;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

    }
}