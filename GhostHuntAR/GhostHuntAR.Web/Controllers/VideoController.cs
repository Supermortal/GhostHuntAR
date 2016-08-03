using System;
using System.Linq;
using System.Web.Mvc;
using GhostHuntAR.Infrastructure.Abstract;
using GhostHuntAR.Infrastructure.Helpers;
using log4net;

namespace GhostHuntAR.Web.Controllers
{
    public class VideoController : Controller
    {

      private static readonly ILog Log = LogHelper.GetLogger
            (typeof(VideoController));

      private IVideoService db;

      public VideoController(IVideoService vs)
      {
        db = vs;
      }

      [HttpPost]
      [Authorize]
      public ActionResult Create(long ghLocationId, string description, string url, string type)
      {
        try
        {
          db.Save(ghLocationId, description, url, UserHelper.Instance.CurrentUserName, type);

          if (Request.UrlReferrer != null && Request.UrlReferrer.AbsoluteUri != null)
            return new RedirectResult(Request.UrlReferrer.AbsoluteUri);
          else
            return RedirectToAction("Index", "Manage");
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
          var v = db.GetVideo(id);

          if (v.UserName != UserHelper.Instance.CurrentUserName)
            return HttpNotFound();

          if (Request.UrlReferrer != null)
            ViewBag.ReferrerUrl = Request.UrlReferrer.AbsoluteUri;

          return View(v);
        }
        catch (Exception ex)
        {
          Log.Error(ex.Message, ex);
          return HttpNotFound();
        }
      }

      [HttpPost]
      [Authorize]
      public ActionResult Edit(long id, string description, string url = null, string referrerUrl = null)
      {
        try
        {
          var oldVideo = db.GetVideo(id);

          if (oldVideo.UserName != UserHelper.Instance.CurrentUserName)
            return new HttpStatusCodeResult(405, "Unauthorized text edit");

          db.Dispose();

          if (!string.IsNullOrEmpty(url))
            db.Update(id, description, url);
          else
            db.Update(id, description);

          if (!string.IsNullOrEmpty(referrerUrl) && referrerUrl == "AJAX")
            return new JsonResult() {Data = new {Status = "OK"}};
          if (Request.UrlReferrer != null && Request.UrlReferrer.AbsoluteUri != null)
            return new RedirectResult(Request.UrlReferrer.AbsoluteUri);

            return RedirectToAction("Index", "Manage");
        }
        catch (Exception ex)
        {
          Log.Error(ex.Message, ex);
          return HttpNotFound();
        }
      }

      [HttpPost]
      [Authorize]
      public ActionResult Delete(long id)
      {
        try
        {
          var oldVideo = db.GetVideo(id);

          if (oldVideo.UserName != UserHelper.Instance.CurrentUserName)
            return new HttpStatusCodeResult(405, "Unauthorized text delete");

          db.Delete(id);

          if (Request.UrlReferrer != null && Request.UrlReferrer.AbsoluteUri != null)
            return new RedirectResult(Request.UrlReferrer.AbsoluteUri);
          else
            return RedirectToAction("Index", "Manage");
        }
        catch (Exception ex)
        {
          Log.Error(ex.Message, ex);
          return HttpNotFound();
        }
      }

      [ChildActionOnly]
      public ActionResult ListByUser()
      {
        try
        {
          var videos = db.FindAllByUserName(UserHelper.Instance.CurrentUserName);

          return PartialView(videos);
        }
        catch (Exception ex)
        {
          Log.Error(ex.Message, ex);
          return HttpNotFound();
        }
      }

      [ChildActionOnly]
      public ActionResult ListByLocation(long id)
      {
        try
        {
          var videos = db.FindAllByGHLocationID(id);

          return PartialView(videos);
        }
        catch (Exception ex)
        {
          Log.Error(ex.Message, ex);
          return HttpNotFound();
        }
      }

      public ActionResult Display(long id)
      {
        try
        {
          var v = db.GetVideo(id);

          return View(v);
        }
        catch (Exception ex)
        {
          Log.Error(ex.Message, ex);
          return HttpNotFound();
        }
      }

      [HttpPost]
      public JsonResult GetPagedVideosByUserName(string id, int page, int pageSize)
      {
        try
        {
          var jr = new JsonResult();
          jr.Data = db.FindAllByUserName(id, page, pageSize).ToList();

          return jr;
        }
        catch (Exception ex)
        {
          Log.Error(ex.Message, ex);
          return null;
        }
      }

      [HttpPost]
      public JsonResult GetPagedVideosByGHLocationId(int id, int page, int pageSize)
      {
        try
        {
          var jr = new JsonResult();
          jr.Data = db.FindAllByGHLocationID(id, page, pageSize).ToList();

          return jr;
        }
        catch (Exception ex)
        {
          Log.Error(ex.Message, ex);
          return null;
        }
      }

    }
}
