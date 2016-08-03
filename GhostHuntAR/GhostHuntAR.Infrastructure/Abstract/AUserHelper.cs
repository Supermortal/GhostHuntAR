using System;
using System.Web;
using GhostHuntAR.Infrastructure.Models;
using log4net;
using Supermortal.Common.NonPCL.Helpers.Log;

namespace GhostHuntAR.Infrastructure.Abstract
{
  public abstract class AUserHelper
  {

    private static readonly ILog Log = LogHelper.GetLogger(typeof (AUserHelper));

    public abstract bool IsAuthenticated { get; }
    public abstract string CurrentUserName { get; }
    public abstract int CurrentUserId { get; }
    public abstract bool IsAdmin { get; }
    public abstract UserProfile CurrentUser { get; protected set; }

    public abstract bool Login(string userName, string password, bool persistCookie = false);

    public abstract void Logout();

    public abstract string CreateUserAndAccount(RegisterModel model, string role = null, HttpPostedFileBase image = null, bool requireConfirmationToken = false);

    public abstract bool ChangePassword(string userName, string currentPassword, string newPassword);

    public abstract void UpdateCurrentUser(UserProfile user);

    public abstract void UpdateCurrentUser(UserProfile user, HttpPostedFileBase image);

    public void DisposeUser()
    {
      try
      {
        CurrentUser = null;
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

    public abstract void Dispose();

  }
}