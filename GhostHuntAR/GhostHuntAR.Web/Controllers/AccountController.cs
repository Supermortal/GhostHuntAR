using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using GhostHuntAR.Infrastructure.Abstract;
using GhostHuntAR.Infrastructure.Helpers;
using GhostHuntAR.Infrastructure.Models;
using GhostHuntAR.Infrastructure.Models.TokenRegister;
using GhostHuntAR.Web.Filters;
using log4net;
using Microsoft.Web.WebPages.OAuth;
using VikingErik.Mvc.ResumingActionResults;
using WebMatrix.WebData;

namespace GhostHuntAR.Web.Controllers
{
    //[Authorize]
    [InitializeSimpleMembership]
    public class AccountController : Controller
    {

      private static readonly ILog Log = LogHelper.GetLogger
          (typeof(AccountController));

      private readonly ITokenRegisterService _trs;
      private readonly IUserRepository _ur;

      public AccountController(ITokenRegisterService trs, IUserRepository ur)
      {
          _trs = trs;
        _ur = ur;
      }

      [AllowAnonymous]
      public ActionResult Index(string id)
      {
        try
        {
          var u = _ur.FindByUserName(id);
          var user = u.SingleOrDefault();
          if (user == null) return HttpNotFound();

          ViewBag.soundPageSize = 6;
          ViewBag.imagePageSize = 6;
          ViewBag.textPageSize = 6;
          ViewBag.videoPageSize = 6;
          ViewBag.locationPageSize = 6;

          return View(user);
        }
        catch (Exception ex)
        {
          Log.Error(ex.Message, ex);
          return HttpNotFound();
        }
      }

      [Authorize]
      [HttpPost]
      public ActionResult Edit(EditUserViewModel euvm, HttpPostedFileBase image)
      {
        try
        {
          var user = UserHelper.Instance.CurrentUser;

          user.Name = euvm.Name;
          user.Email = euvm.Email;
          user.Biography = euvm.Biography;

          UserHelper.Instance.UpdateCurrentUser(user, image);

          return RedirectToAction("Manage", "Account");
        }
        catch (Exception ex)
        {
          Log.Error(ex.Message, ex);
          return new HttpStatusCodeResult(500, ex.Message);
        }
      }

      [AllowAnonymous]
      public ResumingFileStreamResult Image(long id)
      {
        try
        {
          var user = _ur.Find(id);
          if (user == null || user.Image == null) return null;

          var ms = new MemoryStream(user.Image);
          return new ResumingFileStreamResult(ms, "image/png");
        }
        catch (Exception ex)
        {
          Log.Error(ex.Message, ex);
          return null;
        }
      }

      #region Login/Logoff
      //
      // GET: /Account/Login

      [AllowAnonymous]
      public ActionResult Login(string returnUrl)
      {
          ViewBag.ReturnUrl = returnUrl;
          return View();
      }

      //
      // POST: /Account/Login

      [HttpPost]
      [AllowAnonymous]
      [ValidateAntiForgeryToken]
      public ActionResult Login(LoginModel model, string returnUrl)
      {
          if (ModelState.IsValid && UserHelper.Instance.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
          {
              return RedirectToLocal(returnUrl);
          }

          // If we got this far, something failed, redisplay form
          ModelState.AddModelError("", "The user name or password provided is incorrect.");
          return View(model);
      }

      //
      // POST: /Account/LogOff

      [HttpPost]
      [Authorize]
      [ValidateAntiForgeryToken]
      public ActionResult LogOff()
      {
          UserHelper.Instance.Logout();

          return RedirectToAction("Index", "Home", new {id = string.Empty} );
      }
      #endregion

      #region Manage
      [Authorize]
      public ActionResult Manage(ManageMessageId? message)
    {
      try
      {
        ViewBag.HasLocalPassword = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));

        return View(UserHelper.Instance.CurrentUser);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return HttpNotFound();
      }
    }

      ////
      //// GET: /Account/Manage

      //[Authorize]
      //public ActionResult Manage(ManageMessageId? message)
      //{
      //  ViewBag.StatusMessage =
      //      message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
      //      : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
      //      : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
      //      : "";
      //  ViewBag.HasLocalPassword = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
      //  ViewBag.ReturnUrl = Url.Action("Manage");
      //  return View();
      //}

      ////
      //// POST: /Account/Manage

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public ActionResult ChangePassword(LocalPasswordModel model)
    {
      bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
      ViewBag.HasLocalPassword = hasLocalAccount;
      ViewBag.ReturnUrl = Url.Action("Manage");
      if (hasLocalAccount)
      {
        if (ModelState.IsValid)
        {
          // ChangePassword will throw an exception rather than return false in certain failure scenarios.
          bool changePasswordSucceeded;
          try
          {
            changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
          }
          catch (Exception)
          {
            changePasswordSucceeded = false;
          }

          if (changePasswordSucceeded)
          {
            return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
          }
          else
          {
            ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
          }
        }
      }
      else
      {
        // User does not have a local password so remove any validation errors caused by a missing
        // OldPassword field
        ModelState state = ModelState["OldPassword"];
        if (state != null)
        {
          state.Errors.Clear();
        }

        if (ModelState.IsValid)
        {
          try
          {
            WebSecurity.CreateAccount(User.Identity.Name, model.NewPassword);
            return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
          }
          catch (Exception e)
          {
            ModelState.AddModelError("", e);
          }
        }
      }

      // If we got this far, something failed, redisplay form
      return View(model);
    }
      #endregion

      #region Register
      ////
      //// GET: /Account/Register

      ////[AllowAnonymous]
      //[Authorize]
      //public ActionResult Register()
      //{
      //    return View();
      //}

      //
      // GET: /Account/Register

      [AllowAnonymous]
      public ActionResult Register(string id)
      {
          if (string.IsNullOrEmpty(id))
              return HttpNotFound();

          var pu = _trs.FindPotentialUserFromToken(id);
          if (pu == null)
              return HttpNotFound();

        if (pu.SignedUp)
          return HttpNotFound();

          ViewBag.token = id;

          var rm = new RegisterModel();
          rm.Email = pu.Email;
          rm.Name = pu.Name;

          return View(rm);
      }

      //
      // POST: /Account/Register

      [HttpPost]
      [AllowAnonymous]
      [ValidateAntiForgeryToken]
      public ActionResult Register(RegisterModel model, string token, HttpPostedFileBase image)
      {
          if (ModelState.IsValid)
          {
              // Attempt to register the user
              try
              {
                  if (string.IsNullOrEmpty(token))
                      return HttpNotFound();

                  var pu = _trs.FindPotentialUserFromToken(token);
                  if (pu == null)
                      return HttpNotFound();

                  pu.SignedUp = true;
                  _trs.UpdatePotentialUser(pu);

                  UserHelper.Instance.CreateUserAndAccount(model, pu.Role, image);

                  UserHelper.Instance.Login(model.UserName, model.Password);
                  return RedirectToAction("Index", "Home");
              }
              catch (MembershipCreateUserException e)
              {
                  ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
              }
          }

          // If we got this far, something failed, redisplay form
          return View(model);
      }
      #endregion

      //#region External Login
      ////
      //// POST: /Account/ExternalLogin

      //[HttpPost]
      //[AllowAnonymous]
      //[ValidateAntiForgeryToken]
      //public ActionResult ExternalLogin(string provider, string returnUrl)
      //{
      //    return new ExternalLoginResult(provider, Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
      //}

      ////
      //// GET: /Account/ExternalLoginCallback

      //[AllowAnonymous]
      //public ActionResult ExternalLoginCallback(string returnUrl)
      //{
      //    AuthenticationResult result = OAuthWebSecurity.VerifyAuthentication(Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
      //    if (!result.IsSuccessful)
      //    {
      //        return RedirectToAction("ExternalLoginFailure");
      //    }

      //    if (OAuthWebSecurity.Login(result.Provider, result.ProviderUserId, createPersistentCookie: false))
      //    {
      //        return RedirectToLocal(returnUrl);
      //    }

      //    if (User.Identity.IsAuthenticated)
      //    {
      //        // If the current user is logged in add the new account
      //        OAuthWebSecurity.CreateOrUpdateAccount(result.Provider, result.ProviderUserId, User.Identity.Name);
      //        return RedirectToLocal(returnUrl);
      //    }
      //    else
      //    {
      //        // User is new, ask for their desired membership name
      //        string loginData = OAuthWebSecurity.SerializeProviderUserId(result.Provider, result.ProviderUserId);
      //        ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(result.Provider).DisplayName;
      //        ViewBag.ReturnUrl = returnUrl;
      //        return View("ExternalLoginConfirmation", new RegisterExternalLoginModel { UserName = result.UserName, ExternalLoginData = loginData });
      //    }
      //}

      ////
      //// POST: /Account/ExternalLoginConfirmation

      //[HttpPost]
      //[AllowAnonymous]
      //[ValidateAntiForgeryToken]
      //public ActionResult ExternalLoginConfirmation(RegisterExternalLoginModel model, string returnUrl)
      //{
      //    string provider = null;
      //    string providerUserId = null;

      //    if (User.Identity.IsAuthenticated || !OAuthWebSecurity.TryDeserializeProviderUserId(model.ExternalLoginData, out provider, out providerUserId))
      //    {
      //        return RedirectToAction("Manage");
      //    }

      //    if (ModelState.IsValid)
      //    {
      //        // Insert a new user into the database
      //        using (EFContext db = new EFContext())
      //        {
      //            UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserName.ToLower() == model.UserName.ToLower());
      //            // Check if user already exists
      //            if (user == null)
      //            {
      //                // Insert name into the profile table
      //                db.UserProfiles.Add(new UserProfile { UserName = model.UserName });
      //                db.SaveChanges();

      //                OAuthWebSecurity.CreateOrUpdateAccount(provider, providerUserId, model.UserName);
      //                OAuthWebSecurity.Login(provider, providerUserId, createPersistentCookie: false);

      //                return RedirectToLocal(returnUrl);
      //            }
      //            else
      //            {
      //                ModelState.AddModelError("UserName", "User name already exists. Please enter a different user name.");
      //            }
      //        }
      //    }

      //    ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(provider).DisplayName;
      //    ViewBag.ReturnUrl = returnUrl;
      //    return View(model);
      //}

      ////
      //// GET: /Account/ExternalLoginFailure

      //[AllowAnonymous]
      //public ActionResult ExternalLoginFailure()
      //{
      //    return View();
      //}

      //[AllowAnonymous]
      //[ChildActionOnly]
      //public ActionResult ExternalLoginsList(string returnUrl)
      //{
      //    ViewBag.ReturnUrl = returnUrl;
      //    return PartialView("_ExternalLoginsListPartial", OAuthWebSecurity.RegisteredClientData);
      //}

      //[ChildActionOnly]
      //public ActionResult RemoveExternalLogins()
      //{
      //    ICollection<OAuthAccount> accounts = OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name);
      //    List<ExternalLogin> externalLogins = new List<ExternalLogin>();
      //    foreach (OAuthAccount account in accounts)
      //    {
      //        AuthenticationClientData clientData = OAuthWebSecurity.GetOAuthClientData(account.Provider);

      //        externalLogins.Add(new ExternalLogin
      //        {
      //            Provider = account.Provider,
      //            ProviderDisplayName = clientData.DisplayName,
      //            ProviderUserId = account.ProviderUserId,
      //        });
      //    }

      //    ViewBag.ShowRemoveButton = externalLogins.Count > 1 || OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
      //    return PartialView("_RemoveExternalLoginsPartial", externalLogins);
      //}

      ////
      //// POST: /Account/Disassociate

      //[HttpPost]
      //[ValidateAntiForgeryToken]
      //[Authorize]
      //public ActionResult Disassociate(string provider, string providerUserId)
      //{
      //  string ownerAccount = OAuthWebSecurity.GetUserName(provider, providerUserId);
      //  ManageMessageId? message = null;

      //  // Only disassociate the account if the currently logged in user is the owner
      //  if (ownerAccount == User.Identity.Name)
      //  {
      //    // Use a transaction to prevent the user from deleting their last login credential
      //    using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
      //    {
      //      bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
      //      if (hasLocalAccount || OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name).Count > 1)
      //      {
      //        OAuthWebSecurity.DeleteAccount(provider, providerUserId);
      //        scope.Complete();
      //        message = ManageMessageId.RemoveLoginSuccess;
      //      }
      //    }
      //  }

      //  return RedirectToAction("Manage", new { Message = message });
      //}
      //#endregion

      #region Potential Users
      private const int PotentialUserPageCount = 10;
      [Authorize(Roles = "admin")]
      public ActionResult PotentialUsers(int page = 1, string type = "NotSignedUp")
      {
          try
          {
              bool? signedUp = null;
              if (type == "SignedUp")
                  signedUp = true;
              else if (type == "NotSignedUp")
                  signedUp = false;

              var pup = new PotentialUserParcel();
              _trs.GetAllPotentialUsersWithCount(ref pup, page, PotentialUserPageCount, signedUp);

              if (pup.Count == null)
                  pup.Count = 0;

              ViewBag.Pagination = new Pagination()
              {
                  CurrentPage = page,
                  PageSize = PotentialUserPageCount,
                  TotalPostsCount = (int)pup.Count,
                  Action = "PotentialUsers",
                  Controller = "Account",
                  Type = type
              };

              return View(pup.PotentialUsers);
          }
          catch (Exception ex)
          {
              Log.Error(ex.Message, ex);
              return HttpNotFound();
          }
      }

      [Authorize(Roles = "admin")]
      public ActionResult PotentialUser(long? id)
      {
          try
          {
              if (Request.UrlReferrer != null)
                  ViewBag.referrerUrl = Request.UrlReferrer.AbsoluteUri;

              var potentialUser = new PotentialUser();
              if (id != null && id > 0)
              {
                  potentialUser = _trs.FindPotentialUser((long)id);
              }

              return View(potentialUser);
          }
          catch (Exception ex)
          {
              Log.Error(ex.Message, ex);
              return HttpNotFound();
          }
      }

      [HttpPost]
      [Authorize(Roles = "admin")]
      public ActionResult PotentialUser(PotentialUser pu, string referrerUrl, string ghRole)
      {
          try
          {
              if (ModelState.IsValid)
              {
                  pu.Role = ghRole;

                  if (pu.PotentialUserID > 0)
                      _trs.UpdatePotentialUser(pu);
                  else
                      _trs.SavePotentialUser(pu);

                  //if (string.IsNullOrEmpty(referrerUrl))
                  return RedirectToAction("PotentialUsers", "Account");
                  //else
                  //    return Redirect(referrerUrl);
              }

              return View(pu);
          }
          catch (Exception ex)
          {
              Log.Error(ex.Message, ex);
              return new HttpStatusCodeResult(500);
          }
      }
      #endregion

      #region Helpers
      private ActionResult RedirectToLocal(string returnUrl)
      {
          if (Url.IsLocalUrl(returnUrl))
          {
              return Redirect(returnUrl);
          }
          else
          {
              return RedirectToAction("Index", "Home");
          }
      }

      public enum ManageMessageId
      {
          ChangePasswordSuccess,
          SetPasswordSuccess,
          RemoveLoginSuccess,
      }

      internal class ExternalLoginResult : ActionResult
      {
          public ExternalLoginResult(string provider, string returnUrl)
          {
              Provider = provider;
              ReturnUrl = returnUrl;
          }

          public string Provider { get; private set; }
          public string ReturnUrl { get; private set; }

          public override void ExecuteResult(ControllerContext context)
          {
              OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
          }
      }

      private static string ErrorCodeToString(MembershipCreateStatus createStatus)
      {
          // See http://go.microsoft.com/fwlink/?LinkID=177550 for
          // a full list of status codes.
          switch (createStatus)
          {
              case MembershipCreateStatus.DuplicateUserName:
                  return "User name already exists. Please enter a different user name.";

              case MembershipCreateStatus.DuplicateEmail:
                  return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

              case MembershipCreateStatus.InvalidPassword:
                  return "The password provided is invalid. Please enter a valid password value.";

              case MembershipCreateStatus.InvalidEmail:
                  return "The e-mail address provided is invalid. Please check the value and try again.";

              case MembershipCreateStatus.InvalidAnswer:
                  return "The password retrieval answer provided is invalid. Please check the value and try again.";

              case MembershipCreateStatus.InvalidQuestion:
                  return "The password retrieval question provided is invalid. Please check the value and try again.";

              case MembershipCreateStatus.InvalidUserName:
                  return "The user name provided is invalid. Please check the value and try again.";

              case MembershipCreateStatus.ProviderError:
                  return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

              case MembershipCreateStatus.UserRejected:
                  return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

              default:
                  return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
          }
      }
      #endregion
    }
}
