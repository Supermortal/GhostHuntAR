using System;
using System.Data;
using System.Linq;
using GhostHuntAR.Infrastructure.Abstract;
using GhostHuntAR.Infrastructure.Models;
using log4net;
using Supermortal.Common.NonPCL.Helpers.Log;

namespace GhostHuntAR.Infrastructure.Concrete
{
  public class EFLastUserSettings : ILastUserSettings
  {

    private static readonly ILog Log = LogHelper.GetLogger(typeof (EFLastUserSettings));

    private EFContext db = new EFContext();

    public IQueryable<LastUserSettings> GetAll()
    {
      try
      {
        return db.LastUserSettings;
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public IQueryable<LastUserSettings> FindAllByUserId(long userId)
    {
      try
      {
        return db.LastUserSettings.Where(l => l.UserId == userId);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public LastUserSettings Find(string id)
    {
      try
      {
        return db.LastUserSettings.Find(id);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public void Insert(LastUserSettings lastUserSettings)
    {
      try
      {
        db.LastUserSettings.Add(lastUserSettings);
        db.SaveChanges();
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

    public void Update(LastUserSettings lastUserSettings)
    {
      try
      {
        db.Entry(lastUserSettings).State = EntityState.Modified;
        db.SaveChanges();
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

    public void Delete(LastUserSettings lastUserSettings)
    {
      try
      {
        db.Entry(lastUserSettings).State = EntityState.Deleted;
        db.SaveChanges();
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

    public void Delete(string id)
    {
      try
      {
        db.Entry(db.LastUserSettings.Find(id)).State = EntityState.Deleted;
        db.SaveChanges();
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