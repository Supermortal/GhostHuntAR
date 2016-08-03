using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GhostHuntAR.Infrastructure.Abstract;
using GhostHuntAR.Infrastructure.Helpers;
using log4net;
using VikingErik.Mvc.ResumingActionResults;

namespace GhostHuntAR.Web.Controllers
{
  public class SoundController : Controller
  {

    private static readonly ILog Log = LogHelper.GetLogger
        (typeof(SoundController));

    private ISoundService db;

    public SoundController(ISoundService ss)
    {
      db = ss;
    }

    //
    // GET: /Sound/

    public ResumingFileStreamResult Index(long id)
    {
      try
      {
        return db.Get(id);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    [HttpPost]
    [Authorize]
    public ActionResult Create(long ghLocationId, HttpPostedFileBase[] sounds, string[] soundDescriptions)
    {
      try
      {
        for (var i = 0; i < sounds.Length; i++)
        {
          if (!string.IsNullOrEmpty(soundDescriptions[i]))
            db.Save(ghLocationId, sounds[i], soundDescriptions[i], UserHelper.Instance.CurrentUserName);
          else
            return null;
        }

        //if (Request.UrlReferrer != null && Request.UrlReferrer.AbsoluteUri != null)
        //return new RedirectResult(Request.UrlReferrer.AbsoluteUri);
        //else
        return RedirectToAction("All", "Manage");
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
        var s = db.GetSound(id);

        if (s.UserName != UserHelper.Instance.CurrentUserName)
          return HttpNotFound();

        if (Request.UrlReferrer != null)
          ViewBag.ReferrerUrl = Request.UrlReferrer.AbsoluteUri;

        return View(s);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return HttpNotFound();
      }
    }

    [HttpPost]
    [Authorize]
    public ActionResult Edit(long id, string soundDescription, string referrerUrl)
    {
      try
      {
        var oldSound = db.GetSound(id);

        if (oldSound.UserName != UserHelper.Instance.CurrentUserName)
          return new HttpStatusCodeResult(405, "Unauthorized sound edit");

        db.Dispose();

        db.Update(id, null, soundDescription);

        if (referrerUrl == "AJAX")
          return new JsonResult() { Data = new { Status = "OK" } };
        if (!string.IsNullOrEmpty(referrerUrl))
          return new RedirectResult(referrerUrl);

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
        var sound = db.GetSound(id);
        if (sound.UserName != UserHelper.Instance.CurrentUserName)
          return new HttpStatusCodeResult(405, "Unauthorized sound delete");

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
        var sounds = db.FindAllByUserName(UserHelper.Instance.CurrentUserName);

        return PartialView(sounds);
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
        var sounds = db.FindAllByGHLocationID(id);

        return PartialView(sounds);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return HttpNotFound();
      }
    }

    [HttpPost]
    public JsonResult GetPagedSoundsByUserName(string id, int page, int pageSize)
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
    public JsonResult GetPagedSoundsByGHLocationId(int id, int page, int pageSize)
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
