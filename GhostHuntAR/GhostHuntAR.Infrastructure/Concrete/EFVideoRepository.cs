using System;
using System.Data;
using System.Linq;
using GhostHuntAR.Infrastructure.Abstract;
using GhostHuntAR.Infrastructure.Models;
using Supermortal.Common.NonPCL.Helpers.Log;

namespace GhostHuntAR.Infrastructure.Concrete
{
  public class EFVideoRepository : IVideoRepository
  {

    private static readonly log4net.ILog Log = LogHelper.GetLogger
        (typeof(EFVideoRepository));

    private EFContext db = new EFContext();
    private readonly IGHLocationRepository _ghlr;

    public EFVideoRepository(IGHLocationRepository ghlr)
    {
      _ghlr = ghlr;
    }

    public IQueryable<Video> GetAll()
    {
      try
      {
        return db.Videos;
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public Video Find(long id)
    {
      try
      {
        return db.Videos.Find(id);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public void Insert(Video video)
    {
      try
      {
        _ghlr.UpdateDateLastModified(video.GHLocationID);

        db.Videos.Add(video);
        db.SaveChanges();
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

    public void Update(Video video)
    {
      try
      {
        _ghlr.UpdateDateLastModified(video.GHLocationID);

        db.Entry(video).State = EntityState.Modified;
        db.SaveChanges();
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

    public void Delete(Video video)
    {
      try
      {
        _ghlr.UpdateDateLastModified(video.GHLocationID);

        db.Entry(video).State = EntityState.Deleted;
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
        Delete(db.Videos.Find(id));
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

    public IQueryable<Video> FindAllByUserName(string userName)
    {
      try
      {
        return db.Videos.Where(v => v.UserName == userName);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public IQueryable<Video> FindAllByUserName(string userName, int page, int pageSize)
    {
      try
      {
        return db.Videos.Where(s => s.UserName.ToLower() == userName.ToLower()).OrderBy(t => t.VideoID).Skip((page - 1) * pageSize).Take(pageSize);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public IQueryable<Video> FindAllByGHLocationID(long id)
    {
      try
      {
        return db.Videos.Where(v => v.GHLocationID == id);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public IQueryable<Video> FindAllByGHLocationID(long id, int page, int pageSize)
    {
      try
      {
        return db.Videos.Where(s => s.GHLocationID == id).OrderBy(t => t.VideoID).Skip((page - 1) * pageSize).Take(pageSize);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
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