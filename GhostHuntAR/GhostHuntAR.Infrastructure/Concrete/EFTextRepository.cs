using System;
using System.Data;
using System.Linq;
using GhostHuntAR.Infrastructure.Abstract;
using GhostHuntAR.Infrastructure.Models;
using Supermortal.Common.NonPCL.Helpers.Log;

namespace GhostHuntAR.Infrastructure.Concrete
{
  public class EFTextRepository : ITextRepository
  {

    private static readonly log4net.ILog Log = LogHelper.GetLogger
        (typeof(EFTextRepository));

    private EFContext db = new EFContext();
    private readonly IGHLocationRepository _ghlr;

    public EFTextRepository(IGHLocationRepository ghlr)
    {
      _ghlr = ghlr;
    }

    public IQueryable<Text> GetAll()
    {
      try
      {
        return db.Texts;
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public IQueryable<Text> FindAllByUserName(string userName)
    {
      try
      {
        return db.Texts.Where(s => s.UserName.ToLower() == userName.ToLower());
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public IQueryable<Text> FindAllByUserName(string userName, int page, int pageSize)
    {
      try
      {
        return db.Texts.Where(s => s.UserName.ToLower() == userName.ToLower()).OrderBy(t => t.TextID).Skip((page - 1) * pageSize).Take(pageSize);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public IQueryable<Text> FindAllByGHLocationID(long id)
    {
      try
      {
        return db.Texts.Where(s => s.GHLocationID == id);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        throw ex;
      }
    }

    public IQueryable<Text> FindAllByGHLocationID(long id, int page, int pageSize)
    {
      try
      {
        return db.Texts.Where(s => s.GHLocationID == id).OrderBy(t => t.TextID).Skip((page - 1) * pageSize).Take(pageSize);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public Text Find(long id)
    {
      try
      {
        return db.Texts.Find(id);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public void Insert(Text text)
    {
      try
      {
        _ghlr.UpdateDateLastModified(text.GHLocationID);

        db.Texts.Add(text);
        db.SaveChanges();
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

    public void Update(Text text)
    {
      try
      {
        _ghlr.UpdateDateLastModified(text.GHLocationID);

        db.Entry(text).State = EntityState.Modified;
        db.SaveChanges();
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

    public void Delete(Text text)
    {
      try
      {
        _ghlr.UpdateDateLastModified(text.GHLocationID);

        db.Entry(text).State = EntityState.Deleted;
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
        Delete(db.Texts.Find(id));
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