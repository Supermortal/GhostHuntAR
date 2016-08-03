using System.Web.Optimization;

namespace GhostHuntAR.Web
{
  public class BundleConfig
  {

    // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
    public static void RegisterBundles(BundleCollection bundles)
    {
      RegisterScripts(bundles);
      RegisterStyles(bundles);

      BundleTable.EnableOptimizations = false;
    }

    public static void RegisterScripts(BundleCollection bundles)
    {

      bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                    "~/Scripts/jquery/jquery-{version}.js",
                    "~/Scripts/nsmuploader.js"));

      bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                  "~/Scripts/jquery/jquery-ui-1.10.4.custom.js"));

      bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                  "~/Scripts/jquery/jquery.unobtrusive*",
                  "~/Scripts/jquery/jquery.validate*"));

      bundles.Add(new ScriptBundle("~/bundles/javascript").Include(
          ));

      // Use the development version of Modernizr to develop with and learn from. Then, when you're
      // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
      bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                  "~/Scripts/modernizr-*"));

      //bundles.Add(new ScriptBundle("~/bundles/xinha").Include(
      //    "~/Scripts/xinha/init.js",
      //    "~/Scripts/xinha/XinhaCore.js",
      //    "~/Scripts/xinha/my_config.js"));

      //bundles.Add(new ScriptBundle("~/bundles/fileupload").Include(
      //    "~/Scripts/jquery_file_upload_blueimp/jquery.ui.widget.js",
      //    "~/Scripts/jquery_file_upload_blueimp/load-image.min.js",
      //    "~/Scripts/jquery_file_upload_blueimp/canvas-to-blob.min.js",
      //    "~/Scripts/jquery_file_upload_blueimp/bootstrap.min.js",
      //    "~/Scripts/jquery_file_upload_blueimp/jquery.iframe-transport.js",
      //    "~/Scripts/jquery_file_upload_blueimp/jquery.fileupload.js",
      //    "~/Scripts/jquery_file_upload_blueimp/jquery.fileupload-process.js",
      //    "~/Scripts/jquery_file_upload_blueimp/jquery.fileupload-image.js",
      //    "~/Scripts/jquery_file_upload_blueimp/jquery.fileupload-audio.js",
      //    "~/Scripts/jquery_file_upload_blueimp/jquery.fileupload-video.js",
      //  //"~/Scripts/jquery_file_upload_blueimp/jquery.fileupload-jquery-ui.js",             
      //    "~/Scripts/jquery_file_upload_blueimp/jquery.fileupload-ui.js",
      //    "~/Scripts/jquery_file_upload_blueimp/jquery.fileupload-validate.js"));

      bundles.Add(new ScriptBundle("~/bundles/dropzone").Include(
          "~/Scripts/dropzone/dropzone.js"));

      bundles.Add(new ScriptBundle("~/bundles/infiniscroll").Include(
          "~/Scripts/infiniscroll/jquery-ias.min.js"));

      bundles.Add(new ScriptBundle("~/bundles/sightsandsoundslazyload").Include(
            "~/Scripts/sightsandsoundslazyload/SightsAndSoundsAjax.js"));

      bundles.Add(new ScriptBundle("~/bundles/home").Include(
              "~/Scripts/sightsandsoundslazyload/SightsAndSoundsAjax.js",
              "~/Scripts/home/UserDashboard.js",
              "~/Scripts/home/init.js"));

      bundles.Add(new ScriptBundle("~/bundles/cryptojs").Include(
            "~/Scripts/cryptojs/sha256.js"));

      bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
        "~/Scripts/knockout-3.1.0.js"));

    }

    public static void RegisterStyles(BundleCollection bundles)
    {

      //main content
      bundles.Add(new StyleBundle("~/Content/css").Include(
        //"~/Content/reset.css",
            "~/Content/Site.css"));

      //home
      bundles.Add(new StyleBundle("~/Content/home").Include(
        "~/Content/home/userdashboard.css",
        "~/Content/home/index.css",
         "~/Content/home/style.css"));

      //manage
      bundles.Add(new StyleBundle("~/Content/manage").Include(
        "~/Content/manage/addsightsandsounds.css",
           "~/Content/manage/edit.css",
           "~/Content/manage/style.css"));

      //account
      bundles.Add(new StyleBundle("~/Content/account").Include(
        "~/Content/account/register.css",
        "~/Content/account/index.css",
        "~/Content/account/potentialusers.css",
        "~/Content/account/potentialuser.css",
             "~/Content/account/style.css",
             "~/Content/account/manage.css"));

      //jquery ui
      bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
            "~/Content/themes/base/jquery-ui-1.10.4.custom.css"));

    }

  }
}