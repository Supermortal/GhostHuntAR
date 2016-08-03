using System;
using System.Linq;
using System.Web.Mvc;
using GhostHuntAR.Infrastructure.Abstract;
using GhostHuntAR.Infrastructure.Helpers;
using GhostHuntAR.Infrastructure.Models;
using log4net;

namespace GhostHuntAR.Web.Controllers
{
  public class TextController : Controller
  {

    private static readonly ILog Log = LogHelper.GetLogger
        (typeof(TextController));

    private ITextService db;

    public TextController(ITextService ts)
    {
      db = ts;
    }

    //
    // GET: /Image/

    public string Index(long id)
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
    public ActionResult Create(long ghLocationId, string title, string textBody)
    {
      try
      {
        db.Save(ghLocationId, title, textBody, UserHelper.Instance.CurrentUserName);

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
        var ft = new FullText();
        ft.Text = db.GetText(id);
        ft.RawText = db.Get(id);

        if (ft.Text.UserName != UserHelper.Instance.CurrentUserName)
          return HttpNotFound();

        return View(ft);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return HttpNotFound();
      }
    }

    [HttpPost]
    [Authorize]
    public ActionResult Edit(long id, string title, string textBody = null, string referrerUrl = null)
    {
      try
      {
        var oldText = db.GetText(id);

        if (oldText.UserName != UserHelper.Instance.CurrentUserName)
          return new HttpStatusCodeResult(405, "Unauthorized text edit");

        db.Dispose();

        if (!string.IsNullOrEmpty(textBody))
          db.Update(id, title, textBody);
        else
          db.Update(id, title);

        if (!string.IsNullOrEmpty(referrerUrl) && referrerUrl == "AJAX")
          return new JsonResult() { Data = new { Status = "OK" } };
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

    [HttpPost]
    [Authorize]
    public ActionResult Delete(long id)
    {
      try
      {
        var oldText = db.GetText(id);

        if (oldText.UserName != UserHelper.Instance.CurrentUserName)
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

    public ActionResult Display(long id)
    {
      try
      {
        var ft = new FullText();
        ft.Text = db.GetText(id);
        ft.RawText = db.Get(id);
        return View(ft);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return HttpNotFound();
      }
    }

    [HttpPost]
    public JsonResult GetPagedTextsByUserName(string id, int page, int pageSize)
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
    public JsonResult GetPagedTextsByGHLocationId(int id, int page, int pageSize)
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
