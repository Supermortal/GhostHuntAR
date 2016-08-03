using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using GhostHuntAR.Infrastructure.Abstract;
using GhostHuntAR.Infrastructure.Helpers;
using GhostHuntAR.Infrastructure.Models.Enums;
using GhostHuntAR.Infrastructure.Models.TransmitModels;
using log4net;
using Supermortal.Common.NonPCL.Helpers.Log;

namespace GhostHuntAR.Web.Controllers
{

    public class HomeController : Controller
    {

        private static readonly ILog Log = LogHelper.GetLogger
            (typeof(HomeController));

        private readonly IGHLocationService _gls;
        private readonly ISearchService _ss;
        private readonly IUserRepository _ur;

        public HomeController(IGHLocationService gls, ISearchService ss, IUserRepository ur)
        {
            _gls = gls;
            _ss = ss;
            _ur = ur;
        }

        [AllowAnonymous]
        public ActionResult Index(string id)
        {
            try
            {
                ViewBag.Id = id;

                return View();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return HttpNotFound();
            }
        }

        [ChildActionOnly]
        public async Task<PartialViewResult> UserDashboard(string id)
        {
            try
            {

                IEnumerable<GHLocationListItemTransmitModel> locations = (new List<GHLocationListItemTransmitModel>()).AsQueryable();
                id = id.ToLower();

                if (!string.IsNullOrEmpty(UserHelper.Instance.CurrentUser.LastUserSettings.LastUserSettingsType) && string.IsNullOrEmpty(id))
                {
                    var type = UserHelper.Instance.CurrentUser.LastUserSettings.LastUserSettingsType;
                    //_ur.ChangeLastUserSettingsType(UserHelper.Instance.CurrentUserId);
                    //UserHelper.Instance.Dispose();

                    if (type == LastUserSettingsTypes.MyEdits.ToString())
                    {
                        locations = _gls.GetAllUserEditedLocations(UserHelper.Instance.CurrentUserName);
                    }
                    else if (type == LastUserSettingsTypes.MyLocations.ToString())
                    {
                        locations = UserHelper.Instance.CurrentUser.GHLocations.Select(l => new GHLocationListItemTransmitModel(l));
                    }
                    else if (type == LastUserSettingsTypes.SavedSearch.ToString())
                    {
                        locations = (await _ss.FromSavedSearch(UserHelper.Instance.CurrentUser.LastUserSettings.SavedSearch)).Select(l => new GHLocationListItemTransmitModel(l));
                    }

                }
                else
                {

                    if (id == "myedits")
                    {
                        _ur.ChangeLastUserSettingsType(UserHelper.Instance.CurrentUserId, LastUserSettingsTypes.MyEdits);
                        UserHelper.Instance.Dispose();
                        locations = _gls.GetAllUserEditedLocations(UserHelper.Instance.CurrentUserName);
                    }
                    else if (id == "mylocations")
                    {
                        _ur.ChangeLastUserSettingsType(UserHelper.Instance.CurrentUserId, LastUserSettingsTypes.MyLocations);
                        UserHelper.Instance.Dispose();
                        locations = UserHelper.Instance.CurrentUser.GHLocations.Select(l => new GHLocationListItemTransmitModel(l));
                    }
                    else
                    {

                        if (UserHelper.Instance.CurrentUser.LastUserSettings.SavedSearch != null)
                        {
                            _ur.ChangeLastUserSettingsType(UserHelper.Instance.CurrentUserId, LastUserSettingsTypes.SavedSearch);
                            UserHelper.Instance.Dispose();
                            locations =
                              (await _ss.FromSavedSearch(UserHelper.Instance.CurrentUser.LastUserSettings.SavedSearch))
                                .Select(l => new GHLocationListItemTransmitModel(l));
                        }
                        else
                        {
                            //return RedirectToAction("Index", "Home", new { id = "MyEdits" });
                            _ur.ChangeLastUserSettingsType(UserHelper.Instance.CurrentUserId, LastUserSettingsTypes.MyEdits);
                            UserHelper.Instance.Dispose();
                            locations = _gls.GetAllUserEditedLocations(UserHelper.Instance.CurrentUserName);
                            return PartialView(locations);
                        }

                    }

                }

                ViewBag.soundPageSize = 10;
                ViewBag.imagePageSize = 10;
                ViewBag.textPageSize = 10;
                ViewBag.videoPageSize = 10;

                return PartialView(locations);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public ActionResult DisplayAdminOptions()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return HttpNotFound();
            }
        }

    }

}
