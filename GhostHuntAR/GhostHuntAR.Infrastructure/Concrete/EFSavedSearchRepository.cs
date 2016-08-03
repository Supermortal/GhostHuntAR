using System;
using System.Data;
using System.Linq;
using GhostHuntAR.Infrastructure.Abstract;
using GhostHuntAR.Infrastructure.Models;
using log4net;
using Supermortal.Common.NonPCL.Helpers.Log;

namespace GhostHuntAR.Infrastructure.Concrete
{
  public class EFSavedSearchRepository : ISavedSearchRepository
  {

    private static readonly ILog Log = LogHelper.GetLogger(typeof (EFSavedSearchRepository));

    private EFContext db = new EFContext();

    public IQueryable<SavedSearch> GetAll()
    {
      try
      {
        return db.SavedSearches;
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public SavedSearch Find(string id)
    {
      try
      {
        return db.SavedSearches.Find(id);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public void Insert(SavedSearch savedSearch)
    {
      try
      {
        db.SavedSearches.Add(savedSearch);
        db.SaveChanges();
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

    public void Update(SavedSearch savedSearch)
    {
      try
      {
        db.Entry(savedSearch).State = EntityState.Modified;
        db.SaveChanges();
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

    public void Delete(SavedSearch savedSearch)
    {
      try
      {
        db.Entry(savedSearch).State = EntityState.Deleted;
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
        db.Entry(db.SavedSearches.Find(id)).State = EntityState.Deleted;
        db.SaveChanges();
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

    public void DeleteByUserId(long userId)
    {
      try
      {
        var parameters = new object[] { userId };

        db.Database.ExecuteSqlCommand(Properties.Resources.DeleteSavedSearchesByUserId, parameters);
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

    public void SetContext(object context)
    {
      try
      {
        db = (EFContext) context;
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

  }
}