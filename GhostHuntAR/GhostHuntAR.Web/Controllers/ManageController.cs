using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GhostHuntAR.Infrastructure.Abstract;
using GhostHuntAR.Infrastructure.Helpers;
using GhostHuntAR.Infrastructure.Models;
using GhostHuntAR.Infrastructure.Models.TransmitModels;
using log4net;
using VikingErik.Mvc.ResumingActionResults;

namespace GhostHuntAR.Web.Controllers
{
  public class ManageController : Controller
  {

    private static readonly ILog Log = LogHelper.GetLogger
        (typeof(ManageController));

    private readonly IGHLocationService db;
    private readonly IUserRepository _users;

    public ManageController(IGHLocationService gls, IUserRepository ur)
    {
      db = gls;
      _users = ur;
    }

    //
    // GET: /ManageLocations/

    [HttpPost]
    public JsonResult Index(long id)
    {
      try
      {
        var jr = new JsonResult();
        jr.Data = new GHLocationListItemTransmitModel(db.FindLocation(id));
        jr.MaxJsonLength = int.MaxValue;

        return jr;
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    [Authorize]
    public ActionResult All()
    {
      try
      {
        var user = UserHelper.Instance.CurrentUser;
        return user != null ? View(user.GHLocations) : View();
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return HttpNotFound();
      }
    }

    [Authorize]
    public ActionResult Create()
    {
      try
      {
        var location = new GHLocation();
        location.State = "Alabama";
        return View(location);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return HttpNotFound();
      }
    }

    [HttpPost]
    [Authorize]
    public ActionResult Create([Bind(Exclude = "Image")]GHLocation location, HttpPostedFileBase image)
    {
      try
      {
        var success = db.SaveLocation(location, image, UserHelper.Instance.CurrentUserId);

        if (success)
        {
          return RedirectToAction("Index", "Home");
        }

        return View(location);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return HttpNotFound();
      }
    }

    [Authorize]
    public ActionResult Edit(long id)
    {
      try
      {
        var location = db.FindLocation(id);

        if (location.CreatedByUserID != UserHelper.Instance.CurrentUserId)
          return new HttpStatusCodeResult(405, "Unauthorized location edit");

        return View(location);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return HttpNotFound();
      }
    }

    [HttpPost]
    [Authorize]
    public ActionResult Edit([Bind(Exclude = "Image")]GHLocation location, HttpPostedFileBase image)
    {
      try
      {
        var oldLocation = db.FindLocation(location.GHLocationID);

        if (oldLocation.CreatedByUserID != UserHelper.Instance.CurrentUserId)
          return new HttpStatusCodeResult(405, "Unauthorized location edit");

        db.Dispose();

        db.UpdateLocation(location, image);
        return RedirectToAction("Index", "Home");
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return HttpNotFound();
      }
    }

    [AllowAnonymous]
    public ActionResult AddSightsAndSounds(long id)
    {
      try
      {
        var location = db.FindLocation(id);
        return View(location);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return HttpNotFound();
      }
    }

    [AllowAnonymous]
    public JsonResult GetLocationTitle(long id)
    {
      try
      {
        var jr = new JsonResult();
        jr.Data = db.GetLocationTitle(id);

        return jr;
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    [AllowAnonymous]
    public ResumingFileStreamResult Image(long id)
    {
      try
      {
        var imageArr = db.GetImage(id);

        return new ResumingFileStreamResult(new MemoryStream(imageArr), "image/png");
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    [AllowAnonymous]
    public JsonResult GetPagedLocationsByUserName(string id, int page, int pageSize)
    {
      try
      {
        var u = _users.FindByUserName(id);
          var user = u.SingleOrDefault();
          if (user == null) return new JsonResult() { Data = new{ Status = "ERROR", Message = "User not found"}};

          return new JsonResult() { Data = db.GetAllUserOwnedLocations(user.UserId, page, pageSize) };
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

  }
}
