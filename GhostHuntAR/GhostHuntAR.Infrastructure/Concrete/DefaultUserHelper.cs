using System;
using System.Linq;
using System.Web;
using System.Web.Security;
using GhostHuntAR.Infrastructure.Abstract;
using GhostHuntAR.Infrastructure.Models;
using Supermortal.Common.NonPCL.Helpers.Log;
using WebMatrix.WebData;

namespace GhostHuntAR.Infrastructure.Concrete
{
  public class DefaultUserHelper : AUserHelper 
  {
    private static readonly log4net.ILog Log = LogHelper.GetLogger
            (typeof(DefaultUserHelper));

    public override bool IsAuthenticated
    {
      get
      {
        return WebSecurity.IsAuthenticated;
      }
    }
    public override string CurrentUserName
    {
      get { return WebSecurity.CurrentUserName; }
    }
    public override int CurrentUserId { get { return WebSecurity.CurrentUserId; } }
    public override bool IsAdmin { get { return Roles.IsUserInRole("admin"); } }

    public override UserProfile CurrentUser {
      get { return (UserProfile)HttpContext.Current.Session["CurrentUser"] ?? (UserProfile)(HttpContext.Current.Session["CurrentUser"] = _ur.Find(CurrentUserId)); }
      protected set { HttpContext.Current.Session["CurrentUser"] = value; }
    }

    private readonly IUserRepository _ur = null;

    public DefaultUserHelper(IUserRepository ur)
    {
      _ur = ur;
    }

    public override bool Login(string userName, string password, bool persistCookie = false)
    {
      try
      {
        CurrentUser = null;
        return WebSecurity.Login(userName, password, persistCookie);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return false;
      }
    }

    public override void Logout()
    {
      try
      {
        CurrentUser = null;
        WebSecurity.Logout();
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

    public override string CreateUserAndAccount(RegisterModel model, string role = null, HttpPostedFileBase image = null, 
      bool requireConfirmationToken = false)
    {
      try
      {
        CurrentUser = null;

        byte[] arr = null;
        if (image != null)
        {
          arr = new byte[image.ContentLength];
          image.InputStream.Read(arr, 0, image.ContentLength);
        }

        var result = string.Empty;
        try
        {
          if (arr != null)
            result = WebSecurity.CreateUserAndAccount(model.UserName, model.Password, new { Role = role, Name = model.Name, Email = model.Email, Image = arr, Biography = model.Biography }, requireConfirmationToken);
          else
            result = WebSecurity.CreateUserAndAccount(model.UserName, model.Password, new { Role = role, Name = model.Name, Email = model.Email, Biography = model.Biography }, requireConfirmationToken);
        }
        catch (MembershipCreateUserException e)
        {
          throw e;
        }

        if (!string.IsNullOrEmpty(role) && Enum.GetNames(typeof(GHRoles)).Contains(role))
        {
          if (!Roles.IsUserInRole(model.UserName))
            Roles.AddUserToRole(model.UserName, role);
        }

        _ur.Dispose();
        var user = _ur.FindByUserName(model.UserName).Single();
        user.LastUserSettings = new LastUserSettings();
        user.LastUserSettings.UserId = user.UserId;
        _ur.Update(user);

        return result;
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public override bool ChangePassword(string userName, string currentPassword, string newPassword)
    {
      try
      {
        CurrentUser = null;
        return WebSecurity.ChangePassword(userName, currentPassword, newPassword);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return false;
      }
    }

    public override void UpdateCurrentUser(UserProfile user)
     {
       UpdateCurrentUser(user, null);
     }

    public override void UpdateCurrentUser(UserProfile user, HttpPostedFileBase image)
    {
      try
      {
        user.UserName = CurrentUser.UserName;
        user.UserId = CurrentUser.UserId;
        user.Image = CurrentUser.Image;

        if (image != null)
        {
          var arr = new byte[image.ContentLength];
          image.InputStream.Read(arr, 0, image.ContentLength);
          user.Image = arr;
        }

        CurrentUser = null;
        _ur.Dispose();
        _ur.Update(user);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

    public override void Dispose()
    {
      CurrentUser = null;
      _ur.Dispose();
    }

  }
}