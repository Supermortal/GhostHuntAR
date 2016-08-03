using System.Web.Mvc;
using GhostHuntAR.Web.Filters;

namespace GhostHuntAR.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new InitializeSimpleMembershipAttribute());
            //filters.Add(new AuthorizeAttribute());
        }
    }
}