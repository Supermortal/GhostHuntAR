using System;
using System.Data;
using System.Linq;
using GhostHuntAR.Infrastructure.Abstract;
using GhostHuntAR.Infrastructure.Models;
using GhostHuntAR.Infrastructure.Models.Enums;
using Supermortal.Common.NonPCL.Helpers.Log;

namespace GhostHuntAR.Infrastructure.Concrete
{
  public class EFUserRepository : IUserRepository
  {

    private static readonly log4net.ILog Log = LogHelper.GetLogger
        (typeof(EFUserRepository));

    private EFContext db = new EFContext();

    public IQueryable<UserProfile> GetAll()
    {
      try
      {
        return db.UserProfiles;
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        throw ex;
      }
    }

    public UserProfile Find(long id)
    {
      try
      {
        return db.UserProfiles.Find(id);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        throw ex;
      }
    }

    public void Delete(UserProfile user)
    {
      try
      {
        db.Entry(user).State = EntityState.Deleted;
        db.SaveChanges();
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        throw ex;
      }
    }

    public void Delete(long id)
    {
      try
      {
        db.Entry(db.UserProfiles.Find(id)).State = EntityState.Deleted;
        db.SaveChanges();
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        throw ex;
      }
    }

    public long Save(UserProfile user)
    {
      throw new Exception("For Entity Framework User Repository, Please Use WebSecurity.CreateUserAndAccount");
    }

    public void Update(UserProfile user)
    {
      try
      {
        db.Entry(user).State = EntityState.Modified;
        db.SaveChanges();
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
        db = (EFContext)context;
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        throw ex;
      }
    }

    public IQueryable<UserProfile> FindByUserName(string userName, bool useContains)
    {
      try
      {
        return useContains ? db.UserProfiles.Where(u => u.UserName.ToLower().Contains(userName.ToLower())) : db.UserProfiles.Where(u => u.UserName == userName);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        throw ex;
      }
    }

    public IQueryable<UserProfile> FindByUserName(string userName)
    {
      try
      {
        return FindByUserName(userName, false);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public IQueryable<UserProfile> FindByRole(string role)
    {
      try
      {
        return db.UserProfiles.Where(u => u.Role.ToLower() == role.ToLower());
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        throw ex;
      }
    }

    public IQueryable<UserProfile> FindByEmail(string email)
    {
      try
      {
        return db.UserProfiles.Where(u => u.Email.ToLower() == email.ToLower());
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        throw ex;
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

    public object GetContext()
    {
      return db;
    }

    public void DeleteSearch(UserProfile user)
    {
      try
      {
        if (user.LastUserSettings != null && user.LastUserSettings.SavedSearch != null)
        {
          db.Entry(user.LastUserSettings.SavedSearch).State = EntityState.Deleted;
          db.SaveChanges();

          Update(user);
        }
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

    public void SaveSearch(long userId, SavedSearch search)
    {
      var user = Find(userId);

      DeleteSearch(user);

      user.LastUserSettings.SavedSearch = search;
      Update(user);
    }

    public void DeleteLastUserSettings(UserProfile user)
    {
      try
      {
        if (user.LastUserSettings != null)
        {
          db.Entry(user.LastUserSettings).State = EntityState.Deleted;
          db.SaveChanges();

          Update(user);
        }
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

    public void ChangeLastUserSettingsType(long userId, LastUserSettingsTypes type)
    {
      try
      {
        var user = Find(userId);

        user.LastUserSettings.LastUserSettingsType = type.ToString();
        Update(user);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

    public void ChangeLastUserSettingsType(long userId)
    {
      try
      {
        var user = Find(userId);

        user.LastUserSettings.LastUserSettingsType = null;
        Update(user);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

  }
}