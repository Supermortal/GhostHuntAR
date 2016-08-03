using System;
using System.Web.Mvc;
using GhostHuntAR.Infrastructure.Abstract;
using GhostHuntAR.Infrastructure.Helpers;
using log4net;
using Ninject;
using Supermortal.Common.NonPCL.Helpers.Log;

namespace GhostHuntAR.Web.Controllers
{
  [Authorize(Roles = "admin")]
  public class EmailController : Controller
  {

    private static readonly ILog Log = LogHelper.GetLogger
    (typeof(EmailController));

    private readonly ASMTPEmailService _ses;
    private readonly IEmailTemplateService _ets;
    private readonly IPotentialUserRepository _pur;

    public EmailController([Named("Unsecure")]ASMTPEmailService ses, IEmailTemplateService ets, IPotentialUserRepository pur)
    {
      _ses = ses;
      _ets = ets;
      _pur = pur;
    }

    public ActionResult SendPotentialUserEmail(long id)
    {
      try
      {
        var pu = _pur.Find(id);
        var template = string.Empty;
        using (_ets)
        {
          _ets.Configure("..\\GhostHuntAR.Infrastructure\\Email\\Templates");
          var bindings = _ets.GetTemplateBindings("TestTemplate");
          bindings["PotentialUserName"] = pu.Name;
          bindings["PotentialUserEmail"] = pu.Email;
          bindings["PotentialUserRole"] = pu.Role;
          bindings["UserName"] = UserHelper.Instance.CurrentUserName;
          bindings["CurrentDate"] = DateTime.Now.ToLongDateString();
          template = _ets.SetTemplateBindings(bindings);
        }

        using (_ses)
        {
          _ses.Login(new SecurityEncryptionProvider(),
            Security.SymmetricEncrypt("LostPassword_NoReply@namespacemedia.com".ToBytes()),
            Security.SymmetricEncrypt("brought$67".ToBytes()));
          _ses.Send(new SMTPEmailMessage()
          {
            Body = template,
            FromAddress = "namespacemedia_test@test.com",
            FromDisplayName = UserHelper.Instance.CurrentUserName,
            Subject = "Test Email From Email Controller",
            ToAddress = pu.Email,
            ToDisplayName = pu.Name,
            IsBodyHtml = true
          });
        }

        return RedirectToAction("PotentialUsers", "Account");
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return new HttpStatusCodeResult(500);
      }
    }

  }
}
