using System;
using System.Data;
using System.Linq;
using GhostHuntAR.Infrastructure.Abstract;
using GhostHuntAR.Infrastructure.Models;
using Supermortal.Common.NonPCL.Helpers.Log;

namespace GhostHuntAR.Infrastructure.Concrete
{
  public class EFImageRepository : IImageRepository
  {

    private static readonly log4net.ILog Log = LogHelper.GetLogger
        (typeof(EFImageRepository));

    private EFContext db = new EFContext();
    private readonly IGHLocationRepository _ghlr;

    public EFImageRepository(IGHLocationRepository ghlr)
    {
      _ghlr = ghlr;
    }

    public IQueryable<Image> GetAll()
    {
      try
      {
        return db.Images;
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public IQueryable<Image> FindAllByUserName(string userName)
    {
      try
      {
        return db.Images.Where(s => s.UserName.ToLower() == userName.ToLower());
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public IQueryable<Image> FindAllByUserName(string userName, int page, int pageSize)
    {
      try
      {
        return db.Images.Where(s => s.UserName.ToLower() == userName.ToLower()).OrderBy(i => i.ImageID).Skip((page - 1) * pageSize).Take(pageSize);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        throw ex;
      }
    }

    public IQueryable<Image> FindAllByGHLocationID(long id)
    {
      try
      {
        return db.Images.Where(s => s.GHLocationID == id);
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
        return db.Images.Where(s => s.GHLocationID == id).OrderBy(i => i.ImageID).Skip((page - 1) * pageSize).Take(pageSize);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        throw ex;
      }
    }

    public Image Find(long id)
    {
      try
      {
        return db.Images.Find(id);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public void Insert(Image image)
    {
      try
      {
        _ghlr.UpdateDateLastModified(image.GHLocationID);

        db.Images.Add(image);
        db.SaveChanges();
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

    public void Update(Image image)
    {
      try
      {
        _ghlr.UpdateDateLastModified(image.GHLocationID);

        db.Entry(image).State = EntityState.Modified;
        db.SaveChanges();
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

    public void Delete(Image image)
    {
      try
      {
        _ghlr.UpdateDateLastModified(image.GHLocationID);

        db.Entry(image).State = EntityState.Deleted;
        db.SaveChanges();
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

    public void Delete(long id)
    {
      try
      {
        Delete(db.Images.Find(id));
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

    public void Dispose()
    {
      try
      {
        db.Dispose();
        db = null;
        db = new EFContext();
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

  }
}