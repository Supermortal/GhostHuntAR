using System;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GhostHuntAR.Infrastructure.Abstract;
using GhostHuntAR.Infrastructure.Models;
using Supermortal.Common.NonPCL.Helpers.Log;

namespace GhostHuntAR.Infrastructure.Concrete
{
  public class EFImageService : IImageService
  {

    private static readonly log4net.ILog Log = LogHelper.GetLogger
        (typeof(EFImageService));

    private IImageRepository _ir;
    private IRawImageRepository _rir;

    public EFImageService(IImageRepository ir, IRawImageRepository rir)
    {
      _ir = ir;
      _rir = rir;
    }

    public string GetContentType(long id)
    {
      try
      {
        return _rir.GetContentType(id);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public FileResult Get(long id)
    {
      try
      {
        var ri = _rir.Find(id);

        return new FileStreamResult(new MemoryStream(ri.Data), ri.MIMEType);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public byte[] GetBytes(long id)
    {
      try
      {
        return _rir.Find(id).Data;
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public Image GetImage(long id)
    {
      try
      {
        return _ir.Find(id);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public void Save(long ghLocationId, HttpPostedFileBase image, string caption, string userName)
    {
      try
      {

        var temp = System.Drawing.Image.FromStream(image.InputStream);
        var ms = new MemoryStream();
        temp.Save(ms, ImageFormat.Png);
        var data = ms.ToArray();

        var i = new Models.Image();
        i.Caption = caption;
        i.UserName = userName;
        i.GHLocationID = ghLocationId;

        ms.Dispose();
        ms = new MemoryStream();
        var myCallback = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
        var thumbNail = temp.GetThumbnailImage(32, 32, myCallback, IntPtr.Zero);
        thumbNail.Save(ms, ImageFormat.Png);
        i.Thumbnail = ms.ToArray();

        _ir.Insert(i);

        var ri = new RawImage();
        ri.FileName = image.FileName;
        ri.MIMEType = "image/png";
        ri.Data = data;
        ri.RawImageID = i.ImageID;

        _rir.Insert(ri);

      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

    public void Update(long imageId, HttpPostedFileBase image, string caption)
    {
      try
      {

        var i = _ir.Find(imageId);
        if (i == null)
          return;
        i.Caption = caption;

        if (image != null)
        {
          var temp = System.Drawing.Image.FromStream(image.InputStream);
          var ms = new MemoryStream();
          temp.Save(ms, ImageFormat.Png);
          var data = ms.ToArray();

          var myCallback = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
          var thumbNail = temp.GetThumbnailImage(16, 16, myCallback, IntPtr.Zero);
          thumbNail.Save(ms, ImageFormat.Png);
          i.Thumbnail = ms.ToArray();

          var ri = _rir.Find(imageId);
          if (ri == null)
            return;
          ri.FileName = image.FileName;
          ri.MIMEType = "image/png";
          ri.Data = data;

          _rir.Update(ri);
        }

        _ir.Update(i);

      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

    public void Delete(long imageId)
    {
      try
      {
        _ir.Delete(imageId);
        _rir.Delete(imageId);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

    public IQueryable<Image> FindAllByUserName(string userName)
    {
      try
      {
        return _ir.FindAllByUserName(userName);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        throw ex;
      }
    }

    public IQueryable<Image> FindAllByUserName(string userName, int page, int pageSize)
    {
      try
      {
        return _ir.FindAllByUserName(userName, page, pageSize);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public IQueryable<Image> FindAllByGHLocationID(long id)
    {
      try
      {
        return _ir.FindAllByGHLocationID(id);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        throw ex;
      }
    }

    public IQueryable<Image> FindAllByGHLocationID(long id, int page, int pageSize)
    {
      try
      {
        return _ir.FindAllByGHLocationID(id, page, pageSize);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        throw ex;
      }
    }

    public bool ThumbnailCallback()
    {
      return false;
    }

    public void Dispose()
    {
      try
      {
        _ir.Dispose();
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

  }
}