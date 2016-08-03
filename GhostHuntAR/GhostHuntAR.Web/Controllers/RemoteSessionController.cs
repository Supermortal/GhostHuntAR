using System;
using System.Web.Mvc;
using GhostHuntAR.Infrastructure.Abstract;
using GhostHuntAR.Infrastructure.Helpers;
using log4net;

namespace GhostHuntAR.Web.Controllers
{
    [AllowAnonymous]
    public class RemoteSessionController : Controller
    {
        //
        // GET: /RemoteSession/

        private static readonly ILog Log = LogHelper.GetLogger
            (typeof(RemoteSessionController));

        private ISessionService _ss;

        public RemoteSessionController(ISessionService ss)
        {
            _ss = ss;
        }

        [HttpPost]
        public JsonResult LogIn(string userName, string password)
        {
           try
           {
               if (UserHelper.Instance.Login(userName, password, true))
               {

                   var session = _ss.CreateSession(userName);

                   var jr = new JsonResult {Data = new { Status = "Success", Token = session.Token }};
                   return jr;
               }
               else
               {
                   var jr = new JsonResult {Data = new {Status = "Failure"}};
                   return jr;
               }
           }
           catch (Exception ex)
           {
               Log.Error(ex.Message, ex);
               var jr = new JsonResult { Data = new {Status = "Error", Message = ex.Message } };
               return jr;
           }
        }

        [HttpPost]
        public JsonResult Test(string token)
        {
            var valid = _ss.ValidateToken(token);
            var user = _ss.GetUser(token);

            var jr = new JsonResult { Data = new { Status = "Success" } };
            return jr;
        }

    }
}
