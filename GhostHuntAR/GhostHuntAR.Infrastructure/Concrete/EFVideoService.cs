using System;
using System.Linq;
using GhostHuntAR.Infrastructure.Abstract;
using GhostHuntAR.Infrastructure.Models;
using Supermortal.Common.NonPCL.Helpers.Log;
using Supermortal.Common.PCL.Helpers;

namespace GhostHuntAR.Infrastructure.Concrete
{
  public class EFVideoService : IVideoService 
  {

    private static readonly log4net.ILog Log = LogHelper.GetLogger
          (typeof(EFVideoService));

    private readonly IVideoRepository db;

    public EFVideoService(IVideoRepository vr)
    {
      db = vr;
    }

    public string Get(long id)
    {
      try
      {
        return db.Find(id).Url;
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public Video GetVideo(long id)
    {
      try
      {
        return db.Find(id);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public void Save(long ghLocationId, string description, string url, string userName, string type)
    {
      try
      {
        var vsup = IoCHelper.Instance.GetNamedBinding<IVideoServiceUrlParser>(type);
        var v = new Video {GHLocationID = ghLocationId, Description = description, Url = vsup.Parse(url), UserName = userName, Type = type};

        db.Insert(v);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

    public void Update(long videoId, string description, string url)
    {
      try
      {
        var v = db.Find(videoId);
        if (v == null) return;

        v.Description = description;
        v.Url = url;

        db.Update(v);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

    public void Update(long videoId, string description)
    {
      try
      {
        var v = db.Find(videoId);
        if (v == null) return;

        v.Description = description;

        db.Update(v);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

    public void Delete(long videoId)
    {
      try
      {
        db.Delete(videoId);
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
        return db.FindAllByUserName(userName);
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
        return db.FindAllByUserName(userName, page, pageSize);
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
        return db.FindAllByGHLocationID(id);
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
        return db.FindAllByGHLocationID(id, page, pageSize);
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
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

  }
}