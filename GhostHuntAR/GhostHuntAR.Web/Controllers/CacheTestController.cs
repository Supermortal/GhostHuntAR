using System;
using System.Globalization;
using System.Web.Mvc;
using GhostHuntAR.Infrastructure.Abstract;
using GhostHuntAR.Infrastructure.Helpers;
using GhostHuntAR.Infrastructure.Models;
using GhostHuntAR.Infrastructure.Models.TransmitModels;

namespace GhostHuntAR.Web.Controllers
{
  [Authorize(Roles = "admin")]
  public class CacheTestController : Controller
  {

    private static readonly log4net.ILog Log = LogHelper.GetLogger
      (typeof(CacheTestController));

    private readonly ISoundRepository _sr;
    private readonly IRawSoundRepository _rsr;
    private readonly ITextRepository _tr;
    private readonly IRawTextRepository _rtr;
    private readonly IImageRepository _ir;
    private readonly IRawImageRepository _rir;
    private readonly IVideoRepository _vr;

    public CacheTestController(ISoundRepository sr,
      IRawSoundRepository rsr,
      ITextRepository tr,
      IRawTextRepository rtr,
      IImageRepository ir,
      IRawImageRepository rir,
      IVideoRepository vr)
    {
      _sr = sr;
      _rsr = rsr;
      _tr = tr;
      _rtr = rtr;
      _ir = ir;
      _rir = rir;
      _vr = vr;
    }

    public ActionResult CacheSizeTest()
    {
      try
      {
        var c1 = CacheHelper<GHLocationTransmitModel>.Instance.GetCacheable("1");
        var c2 = CacheHelper<GHLocationTransmitModel>.Instance.GetCacheable("2");
        var c3 = CacheHelper<GHLocationTransmitModel>.Instance.GetCacheable("3");
        var c4 = CacheHelper<GHLocationTransmitModel>.Instance.GetCacheable("1");

        return View();
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return new HttpStatusCodeResult(500);
      }
    }

    public ActionResult SoundTests()
    {
      try
      {
        var ctm = new CacheTestModel();

        var c = CacheHelper<GHLocationTransmitModel>.Instance.GetCacheable("1");
        if (c == null)
        {
          ctm.Message = "Initial Get Failed";
          return View(ctm);
        }

        var dt = DateTime.Parse(c.DateLastModified, null, DateTimeStyles.AdjustToUniversal);

        var rawSound = new RawSound();
        rawSound.Data = new byte[1];
        rawSound.FileName = "LocationControllerTest";
        rawSound.MIMEType = "audio/mpeg";

        var s = new Sound();
        s.GHLocationID = 1;
        s.Description = "Location Controller Test";
        s.UserName = UserHelper.Instance.CurrentUserName;
        _sr.Insert(s);

        rawSound.RawSoundID = s.SoundID;
        _rsr.Insert(rawSound);

        c = CacheHelper<GHLocationTransmitModel>.Instance.GetCacheable("1");
        if (DateTime.Parse(c.DateLastModified, null, DateTimeStyles.AdjustToUniversal) == dt)
        {
          ctm.Message = "Update Date Last Modified On Sound Insert Failed";
          return View(ctm);
        }
        dt = DateTime.Parse(c.DateLastModified, null, DateTimeStyles.AdjustToUniversal);

        s.Description = "Cache Update Test";
        _sr.Update(s);

        c = CacheHelper<GHLocationTransmitModel>.Instance.GetCacheable("1");
        if (DateTime.Parse(c.DateLastModified, null, DateTimeStyles.AdjustToUniversal) == dt)
        {
          ctm.Message = "Update Date Last Modified On Sound Update Failed";
          return View(ctm);
        }
        dt = DateTime.Parse(c.DateLastModified, null, DateTimeStyles.AdjustToUniversal);

        _sr.Delete(s);
        _rsr.Delete(rawSound);

        c = CacheHelper<GHLocationTransmitModel>.Instance.GetCacheable("1");
        if (DateTime.Parse(c.DateLastModified, null, DateTimeStyles.AdjustToUniversal) == dt)
        {
          ctm.Message = "Update Date Last Modified On Sound Delete Failed";
          return View(ctm);
        }

        c = CacheHelper<GHLocationTransmitModel>.Instance.GetCacheable("1");

        ctm.Message = "Success";

        return View(ctm);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return new HttpStatusCodeResult(500);
      }
    }

    public ActionResult TextTests()
    {
      try
      {
        var ctm = new CacheTestModel();

        var c = CacheHelper<GHLocationTransmitModel>.Instance.GetCacheable("1");
        if (c == null)
        {
          ctm.Message = "Initial Get Failed";
          return View(ctm);
        }

        var dt = DateTime.Parse(c.DateLastModified, null, DateTimeStyles.AdjustToUniversal);

        var rawSound = new RawText();
        rawSound.Body = "";

        var s = new Text();
        s.GHLocationID = 1;
        s.Title = "Location Controller Test";
        s.UserName = UserHelper.Instance.CurrentUserName;
        _tr.Insert(s);

        rawSound.RawTextID = s.TextID;
        _rtr.Insert(rawSound);

        c = CacheHelper<GHLocationTransmitModel>.Instance.GetCacheable("1");
        if (DateTime.Parse(c.DateLastModified, null, DateTimeStyles.AdjustToUniversal) == dt)
        {
          ctm.Message = "Update Date Last Modified On Sound Insert Failed";
          return View(ctm);
        }
        dt = DateTime.Parse(c.DateLastModified, null, DateTimeStyles.AdjustToUniversal);

        s.Title = "Cache Update Test";
        _tr.Update(s);

        c = CacheHelper<GHLocationTransmitModel>.Instance.GetCacheable("1");
        if (DateTime.Parse(c.DateLastModified, null, DateTimeStyles.AdjustToUniversal) == dt)
        {
          ctm.Message = "Update Date Last Modified On Sound Update Failed";
          return View(ctm);
        }
        dt = DateTime.Parse(c.DateLastModified, null, DateTimeStyles.AdjustToUniversal);

        _tr.Delete(s);
        _rtr.Delete(rawSound);

        c = CacheHelper<GHLocationTransmitModel>.Instance.GetCacheable("1");
        if (DateTime.Parse(c.DateLastModified, null, DateTimeStyles.AdjustToUniversal) == dt)
        {
          ctm.Message = "Update Date Last Modified On Sound Delete Failed";
          return View(ctm);
        }

        c = CacheHelper<GHLocationTransmitModel>.Instance.GetCacheable("1");

        ctm.Message = "Success";

        return View(ctm);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return new HttpStatusCodeResult(500);
      }
    }

    public ActionResult VideoTests()
    {
      try
      {
        var ctm = new CacheTestModel();

        var c = CacheHelper<GHLocationTransmitModel>.Instance.GetCacheable("1");
        if (c == null)
        {
          ctm.Message = "Initial Get Failed";
          return View(ctm);
        }

        var dt = DateTime.Parse(c.DateLastModified, null, DateTimeStyles.AdjustToUniversal);

        var s = new Video();
        s.GHLocationID = 1;
        s.Description = "Location Controller Test";
        s.Url = "test";
        s.UserName = UserHelper.Instance.CurrentUserName;
        _vr.Insert(s);

        c = CacheHelper<GHLocationTransmitModel>.Instance.GetCacheable("1");
        if (DateTime.Parse(c.DateLastModified, null, DateTimeStyles.AdjustToUniversal) == dt)
        {
          ctm.Message = "Update Date Last Modified On Sound Insert Failed";
          return View(ctm);
        }
        dt = DateTime.Parse(c.DateLastModified, null, DateTimeStyles.AdjustToUniversal);

        s.Description = "Cache Update Test";
        _vr.Update(s);

        c = CacheHelper<GHLocationTransmitModel>.Instance.GetCacheable("1");
        if (DateTime.Parse(c.DateLastModified, null, DateTimeStyles.AdjustToUniversal) == dt)
        {
          ctm.Message = "Update Date Last Modified On Sound Update Failed";
          return View(ctm);
        }
        dt = DateTime.Parse(c.DateLastModified, null, DateTimeStyles.AdjustToUniversal);

        _vr.Delete(s);

        c = CacheHelper<GHLocationTransmitModel>.Instance.GetCacheable("1");
        if (DateTime.Parse(c.DateLastModified, null, DateTimeStyles.AdjustToUniversal) == dt)
        {
          ctm.Message = "Update Date Last Modified On Sound Delete Failed";
          return View(ctm);
        }

        c = CacheHelper<GHLocationTransmitModel>.Instance.GetCacheable("1");

        ctm.Message = "Success";

        return View(ctm);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return new HttpStatusCodeResult(500);
      }
    }

    public ActionResult ImageTests()
    {
      try
      {
        var ctm = new CacheTestModel();

        var c = CacheHelper<GHLocationTransmitModel>.Instance.GetCacheable("1");
        if (c == null)
        {
          ctm.Message = "Initial Get Failed";
          return View(ctm);
        }

        var dt = DateTime.Parse(c.DateLastModified, null, DateTimeStyles.AdjustToUniversal);

        var rawSound = new RawImage();
        rawSound.Data = new byte[1];
        rawSound.FileName = "LocationControllerTest";
        rawSound.MIMEType = "image/png";

        var s = new Image();
        s.GHLocationID = 1;
        s.Caption = "Location Controller Test";
        s.UserName = UserHelper.Instance.CurrentUserName;
        _ir.Insert(s);

        rawSound.RawImageID = s.ImageID;
        _rir.Insert(rawSound);

        c = CacheHelper<GHLocationTransmitModel>.Instance.GetCacheable("1");
        if (DateTime.Parse(c.DateLastModified, null, DateTimeStyles.AdjustToUniversal) == dt)
        {
          ctm.Message = "Update Date Last Modified On Sound Insert Failed";
          return View(ctm);
        }
        dt = DateTime.Parse(c.DateLastModified, null, DateTimeStyles.AdjustToUniversal);

        s.Caption = "Cache Update Test";
        _ir.Update(s);

        c = CacheHelper<GHLocationTransmitModel>.Instance.GetCacheable("1");
        if (DateTime.Parse(c.DateLastModified, null, DateTimeStyles.AdjustToUniversal) == dt)
        {
          ctm.Message = "Update Date Last Modified On Sound Update Failed";
          return View(ctm);
        }
        dt = DateTime.Parse(c.DateLastModified, null, DateTimeStyles.AdjustToUniversal);

        _ir.Delete(s);
        _rir.Delete(rawSound);

        c = CacheHelper<GHLocationTransmitModel>.Instance.GetCacheable("1");
        if (DateTime.Parse(c.DateLastModified, null, DateTimeStyles.AdjustToUniversal) == dt)
        {
          ctm.Message = "Update Date Last Modified On Sound Delete Failed";
          return View(ctm);
        }

        c = CacheHelper<GHLocationTransmitModel>.Instance.GetCacheable("1");

        ctm.Message = "Success";

        return View(ctm);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return new HttpStatusCodeResult(500);
      }
    }

    public ActionResult HashedQueueTests()
    {
      try
      {
        var hq = new LinkedHashQueue<string>();
        hq.Enqueue("one");
        hq.Enqueue("two");
        hq.Enqueue("three");
        hq.Enqueue("four");
        hq.Enqueue("five");

        foreach (var s in hq)
        {
          System.Diagnostics.Debug.WriteLine(s);
        }

        var temp = hq.Dequeue();

        System.Diagnostics.Debug.WriteLine(string.Empty);
        foreach (var s in hq)
        {
          System.Diagnostics.Debug.WriteLine(s);
        }

        hq.Enqueue("four");

        System.Diagnostics.Debug.WriteLine(string.Empty);
        foreach (var s in hq)
        {
          System.Diagnostics.Debug.WriteLine(s);
        }

        hq.Clear();
        hq.Enqueue("one");
        hq.Enqueue("two");
        hq.Enqueue("three");
        hq.Enqueue("four");
        hq.Enqueue("five");

        hq.Enqueue("one");

        System.Diagnostics.Debug.WriteLine(string.Empty);
        foreach (var s in hq)
        {
          System.Diagnostics.Debug.WriteLine(s);
        }

        hq.Clear();
        hq.Enqueue("one");
        hq.Enqueue("two");
        hq.Enqueue("three");
        hq.Enqueue("four");
        hq.Enqueue("five");

        hq.Enqueue("five");

        System.Diagnostics.Debug.WriteLine(string.Empty);
        foreach (var s in hq)
        {
          System.Diagnostics.Debug.WriteLine(s);
        }

        hq.Clear();
        hq.Enqueue("one");
        hq.Enqueue("two");
        hq.Enqueue("three");
        hq.Enqueue("four");
        hq.Enqueue("five");

        hq.Enqueue("three");

        System.Diagnostics.Debug.WriteLine(string.Empty);
        foreach (var s in hq)
        {
          System.Diagnostics.Debug.WriteLine(s);
        }

        hq.Clear();
        hq.Enqueue("one");
        hq.Enqueue("two");
        hq.Enqueue("three");
        hq.Enqueue("four");
        hq.Enqueue("five");

        hq.Enqueue("four");
        hq.Enqueue("three");

        System.Diagnostics.Debug.WriteLine(string.Empty);
        foreach (var s in hq)
        {
          System.Diagnostics.Debug.WriteLine(s);
        }

        hq.Clear();
        hq.Enqueue("one");
        hq.Enqueue("two");
        hq.Enqueue("three");
        hq.Enqueue("four");
        hq.Enqueue("five");

        temp = hq.Dequeue();
        temp = hq.Dequeue();

        System.Diagnostics.Debug.WriteLine(string.Empty);
        foreach (var s in hq)
        {
          System.Diagnostics.Debug.WriteLine(s);
        }

        return View();
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return new HttpStatusCodeResult(500);
      }
    }

    [HttpPost]
    [AllowAnonymous]
    public JsonResult LoadTestCall(long id)
    {
      try
      {
        var value = CacheHelper<GHLocationTransmitModel>.
        Instance.GetCacheable(id.ToString());

        return new JsonResult()
        {
          Data = new { Status = "OK", Value = value },
          MaxJsonLength = 999999999
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return new JsonResult() { Data = new { Status = "ERROR" }};
      }
    }

    public ActionResult LoadTest()
    {
      try
      {
        return View();
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return new HttpStatusCodeResult(500);
      }
    }

    [HttpPost]
    [AllowAnonymous]
    public JsonResult KillLoadTest()
    {
      try
      {
        CacheHelper<GHLocationTransmitModel>.
          Instance.Dispose();

        return new JsonResult()
        {
          Data = new { Status = "OK" },
          MaxJsonLength = 999999999
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return new JsonResult() { Data = new { Status = "ERROR" } };
      }
    }

    [HttpPost]
    [AllowAnonymous]
    public JsonResult StartCacheHelper()
    {
      try
      {
        var instance = CacheHelper<GHLocationTransmitModel>.
          Instance;

        return new JsonResult()
        {
          Data = new { Status = "OK" },
          MaxJsonLength = 999999999
        };
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return new JsonResult() { Data = new { Status = "ERROR" } };
      }
    }

    public ActionResult RestTest()
    {
      try
      {
        return View();
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return new HttpStatusCodeResult(500);
      }
    }

  }

  public class CacheTestModel
  {
    public string Message { get; set; }
  }

}
