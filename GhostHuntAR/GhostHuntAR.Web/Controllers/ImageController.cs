using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GhostHuntAR.Infrastructure.Abstract;
using GhostHuntAR.Infrastructure.Helpers;
using GhostHuntAR.Infrastructure.Models.TransmitModels;
using log4net;
using VikingErik.Mvc.ResumingActionResults;

namespace GhostHuntAR.Web.Controllers
{
  public class ImageController : Controller
  {

    private static readonly ILog Log = LogHelper.GetLogger
        (typeof(ImageController));

    private IImageService db;

    public ImageController(IImageService _is)
    {
      db = _is;
    }

    //
    // GET: /Image/

    public FileResult Index(long id)
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
    public ActionResult Create(long ghLocationId, HttpPostedFileBase[] images, string[] imageCaptions)
    {
      try
      {
        for (var i = 0; i < images.Length; i++)
        {
          if (!string.IsNullOrEmpty(imageCaptions[i]))
            db.Save(ghLocationId, images[i], imageCaptions[i], UserHelper.Instance.CurrentUserName);
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
        if (Request.UrlReferrer != null)
          ViewBag.ReferrerUrl = Request.UrlReferrer.AbsoluteUri;

        var i = db.GetImage(id);

        if (i.UserName != UserHelper.Instance.CurrentUserName)
          return HttpNotFound();

        return View(i);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return HttpNotFound();
      }
    }

    [HttpPost]
    [Authorize]
    public ActionResult Edit(long id, string imageCaption, string referrerUrl)
    {
      try
      {
        var oldImage = db.GetImage(id);

        if (oldImage.UserName != UserHelper.Instance.CurrentUserName)
          return new HttpStatusCodeResult(405, "unauthorized image edit");

        db.Dispose();

        db.Update(id, null, imageCaption);

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
        var image = db.GetImage(id);
        if (image.UserName != UserHelper.Instance.CurrentUserName)
          return new HttpStatusCodeResult(405, "unauthorized image delete");

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

    public ResumingFileStreamResult ThumbNail(long id)
    {
      try
      {
        var i = db.GetImage(id);
        var contentType = db.GetContentType(id);

        var ms = new MemoryStream(i.Thumbnail);
        return new ResumingFileStreamResult(ms, contentType);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    [HttpPost]
    public JsonResult GetPagedImagesByUserName(string id, int page, int pageSize)
    {
      try
      {
        var images = db.FindAllByUserName(id, page, pageSize).ToList();
        var itms = images.Select(i => new ImageTransmitModel(i)).ToList();

        var jr = new JsonResult();
        jr.MaxJsonLength = int.MaxValue;
        jr.Data = itms;

        return jr;
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    [HttpPost]
    public JsonResult GetPagedImagesByGHLocationId(int id, int page, int pageSize)
    {
      try
      {
        var images = db.FindAllByGHLocationID(id, page, pageSize).ToList();
        var itms = images.Select(i => new ImageTransmitModel(i)).ToList();

        var jr = new JsonResult();
        jr.MaxJsonLength = int.MaxValue;
        jr.Data = itms;

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
