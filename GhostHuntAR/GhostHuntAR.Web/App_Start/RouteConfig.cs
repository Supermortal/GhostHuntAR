using System.Web.Mvc;
using System.Web.Routing;

namespace GhostHuntAR.Web
{
  public class RouteConfig
  {
    public static void RegisterRoutes(RouteCollection routes)
    {
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

      routes.MapRoute(
              name: "",
              url: "Manage/Image/{id}",
              defaults: new { controller = "Manage", action = "Image", id = UrlParameter.Optional }
          );

      routes.MapRoute(
            name: "",
            url: "Manage/Create",
            defaults: new { controller = "Manage", action = "Create" }
        );

      routes.MapRoute(
          name: "",
          url: "Manage/GetLocationTitle/{id}",
          defaults: new { controller = "Manage", action = "GetLocationTitle", id = UrlParameter.Optional }
      );

      routes.MapRoute(
              name: "",
              url: "Manage/All",
              defaults: new { controller = "Manage", action = "All" }
          );

      routes.MapRoute(
            name: "",
            url: "Manage/{id}",
            defaults: new { controller = "Manage", action = "Index", id = UrlParameter.Optional }
        );

      routes.MapRoute(
          name: "",
          url: "Sound/GetPagedSoundsByUserName/{page}/{pageSize}",
          defaults: new { controller = "Sound", action = "GetPagedSoundsByUserName", page = UrlParameter.Optional, pageSize = UrlParameter.Optional }
      );

      routes.MapRoute(
          name: "",
          url: "Sound/GetPagedSoundsByGHLocationId/{id}/{page}/{pageSize}",
          defaults: new { controller = "Sound", action = "GetPagedSoundsByGHLocationId", id = UrlParameter.Optional, page = UrlParameter.Optional, pageSize = UrlParameter.Optional }
      );

      routes.MapRoute(
            name: "",
            url: "Sound/Delete/{id}",
            defaults: new { controller = "Sound", action = "Delete", id = UrlParameter.Optional }
        );

      routes.MapRoute(
          name: "",
          url: "Sound/Create",
          defaults: new { controller = "Sound", action = "Create", id = UrlParameter.Optional }
      );

      routes.MapRoute(
          name: "",
          url: "Sound/Manage",
          defaults: new { controller = "Sound", action = "Manage" }
      );

      routes.MapRoute(
          name: "",
          url: "Sound/Progress/{fileName}",
          defaults: new { controller = "Sound", action = "Progress" }
      );

      routes.MapRoute(
          name: "",
          url: "Sound/{id}",
          defaults: new { controller = "Sound", action = "Index", id = UrlParameter.Optional }
      );

      routes.MapRoute(
          name: "",
          url: "Sound/{action}/{id}",
          defaults: new { controller = "Sound", action = "Index", id = UrlParameter.Optional }
      );


      routes.MapRoute(
          name: "",
          url: "Image/GetPagedImagesByUserName/{page}/{pageSize}",
          defaults: new { controller = "Image", action = "GetPagedImagesByUserName", page = UrlParameter.Optional, pageSize = UrlParameter.Optional }
      );

      routes.MapRoute(
          name: "",
          url: "Image/GetPagedImagesByGHLocationId/{id}/{page}/{pageSize}",
          defaults: new { controller = "Image", action = "GetPagedImagesByGHLocationId", id = UrlParameter.Optional, page = UrlParameter.Optional, pageSize = UrlParameter.Optional }
      );

      routes.MapRoute(
              name: "",
              url: "Image/Delete/{id}",
              defaults: new { controller = "Image", action = "Delete", id = UrlParameter.Optional }
          );

      routes.MapRoute(
          name: "",
          url: "Image/Create",
          defaults: new { controller = "Image", action = "Create", id = UrlParameter.Optional }
      );

      routes.MapRoute(
          name: "",
          url: "Image/Manage",
          defaults: new { controller = "Image", action = "Manage" }
      );

      routes.MapRoute(
          name: "",
          url: "Image/ThumbNail/{id}",
          defaults: new { controller = "Image", action = "ThumbNail", id = UrlParameter.Optional }
      );

      routes.MapRoute(
          name: "",
          url: "Image/{id}",
          defaults: new { controller = "Image", action = "Index", id = UrlParameter.Optional }
      );

      routes.MapRoute(
          name: "",
          url: "Image/{action}/{id}",
          defaults: new { controller = "Image", action = "Index", id = UrlParameter.Optional }
      );


      routes.MapRoute(
          name: "",
          url: "Text/GetPagedTextsByUserName/{page}/{pageSize}",
          defaults: new { controller = "Text", action = "GetPagedTextsByUserName", page = UrlParameter.Optional, pageSize = UrlParameter.Optional }
      );

      routes.MapRoute(
          name: "",
          url: "Text/GetPagedTextsByGHLocationId/{id}/{page}/{pageSize}",
          defaults: new { controller = "Text", action = "GetPagedTextsByGHLocationId", id = UrlParameter.Optional, page = UrlParameter.Optional, pageSize = UrlParameter.Optional }
      );

      routes.MapRoute(
              name: "",
              url: "Text/Delete/{id}",
              defaults: new { controller = "Text", action = "Delete", id = UrlParameter.Optional }
          );

      routes.MapRoute(
          name: "",
          url: "Text/Create",
          defaults: new { controller = "Text", action = "Create", id = UrlParameter.Optional }
      );

      routes.MapRoute(
          name: "",
          url: "Text/Manage",
          defaults: new { controller = "Text", action = "Manage" }
      );

      routes.MapRoute(
          name: "",
          url: "Text/Display/{id}",
          defaults: new { controller = "Text", action = "Display", id = UrlParameter.Optional }
      );

      routes.MapRoute(
          name: "",
          url: "Text/{id}",
          defaults: new { controller = "Text", action = "Index", id = UrlParameter.Optional }
      );

      routes.MapRoute(
          name: "",
          url: "Text/{action}/{id}",
          defaults: new { controller = "Text", action = "Index", id = UrlParameter.Optional }
      );

      routes.MapRoute(
            name: "",
            url: "Account/Login",
            defaults: new { controller = "Account", action = "Login" }
        );

      routes.MapRoute(
            name: "",
            url: "Account/LogOff",
            defaults: new { controller = "Account", action = "LogOff" }
        );

      routes.MapRoute(
              name: "",
              url: "Account/Manage/{message}",
              defaults: new { controller = "Account", action = "Manage", message = UrlParameter.Optional }
          );

      routes.MapRoute(
                name: "",
                url: "Account/Register/{id}",
                defaults: new { controller = "Account", action = "Register", id = UrlParameter.Optional }
            );

      routes.MapRoute(
                name: "",
                url: "Account/PotentialUsers",
                defaults: new { controller = "Account", action = "PotentialUsers" }
            );

      routes.MapRoute(
                name: "",
                url: "Account/PotentialUser/{id}",
                defaults: new { controller = "Account", action = "PotentialUser", id = UrlParameter.Optional }
            );

      routes.MapRoute(
                  name: "",
                  url: "Account/Edit",
                  defaults: new { controller = "Account", action = "Edit" }
              );

      routes.MapRoute(
                    name: "",
                    url: "Account/ChangePassword",
                    defaults: new { controller = "Account", action = "ChangePassword" }
                );

      routes.MapRoute(
                      name: "",
                      url: "Account/Image/{id}",
                      defaults: new { controller = "Account", action = "Image" }
                  );

      routes.MapRoute(
          name: "",
          url: "Account/{id}",
          defaults: new { controller = "Account", action = "Index" }
      );

      routes.MapRoute(
            name: "",
            url: "SavedSearch",
            defaults: new { controller = "Home", action = "Index", id = "SavedSearch" }
        );

      routes.MapRoute(
            name: "",
            url: "MyLocations",
            defaults: new { controller = "Home", action = "Index", id = "MyLocations" }
        );

      routes.MapRoute(
            name: "",
            url: "MyEdits",
            defaults: new { controller = "Home", action = "Index", id = "MyEdits" }
        );


      routes.MapRoute(
            name: "",
            url: "",
            defaults: new { controller = "Home", action = "Index", id = string.Empty }
        );

      routes.MapRoute(
          name: "",
          url: "{controller}/{action}/{id}",
          defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
      );

      routes.MapRoute(
          name: "",
          url: "{controller}/{action}/{type}/{page}",
          defaults: new { controller = "Home", action = "Index", type = UrlParameter.Optional, page = UrlParameter.Optional }
      );

    }
  }
}