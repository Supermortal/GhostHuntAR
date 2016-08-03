using System;
using System.Data;
using System.Linq;
using GhostHuntAR.Infrastructure.Abstract;
using GhostHuntAR.Infrastructure.Models;
using Supermortal.Common.NonPCL.Helpers.Log;

namespace GhostHuntAR.Infrastructure.Concrete
{
  public class EFSoundRepository : ISoundRepository
  {

    private static readonly log4net.ILog Log = LogHelper.GetLogger
        (typeof(EFSoundRepository));

    private EFContext db = new EFContext();
    private readonly IGHLocationRepository _ghlr;

    public EFSoundRepository(IGHLocationRepository ghlr)
    {
      _ghlr = ghlr;
    }

    public IQueryable<Sound> GetAll()
    {
      try
      {
        return db.Sounds;
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public IQueryable<Sound> FindAllByUserName(string userName)
    {
      try
      {
        return db.Sounds.Where(s => s.UserName.ToLower() == userName.ToLower());
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public IQueryable<Sound> FindAllByUserName(string userName, int page, int pageSize)
    {
      try
      {
        return db.Sounds.Where(s => s.UserName.ToLower() == userName.ToLower()).OrderBy(s => s.SoundID).Skip((page - 1) * pageSize).Take(pageSize);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public IQueryable<Sound> FindAllByGHLocationID(long id)
    {
      try
      {
        return db.Sounds.Where(s => s.GHLocationID == id);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        throw ex;
      }
    }

    public IQueryable<Sound> FindAllByGHLocationID(long id, int page, int pageSize)
    {
      try
      {
        return db.Sounds.Where(s => s.GHLocationID == id).OrderBy(s => s.SoundID).Skip((page - 1) * pageSize).Take(pageSize);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public Sound Find(long id)
    {
      try
      {
        return db.Sounds.Find(id);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public void Insert(Sound sound)
    {
      try
      {
        _ghlr.UpdateDateLastModified(sound.GHLocationID);

        db.Sounds.Add(sound);
        db.SaveChanges();
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

    public void Update(Sound sound)
    {
      try
      {
        _ghlr.UpdateDateLastModified(sound.GHLocationID);

        db.Entry(sound).State = EntityState.Modified;
        db.SaveChanges();
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

    public void Delete(Sound sound)
    {
      try
      {
        _ghlr.UpdateDateLastModified(sound.GHLocationID);

        db.Entry(sound).State = EntityState.Deleted;
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
        Delete(db.Sounds.Find(id));
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