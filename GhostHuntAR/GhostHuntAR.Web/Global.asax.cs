using System;
using System.IO;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using GhostHuntAR.Infrastructure.Abstract;
using GhostHuntAR.Infrastructure.Concrete;
using GhostHuntAR.Infrastructure.Models.TransmitModels;
using log4net;

namespace GhostHuntAR.Web
{
  // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
  // visit http://go.microsoft.com/?LinkId=9394801

  public class WebApiApplication : System.Web.HttpApplication
  {

    private static ILog _log;

    protected void Application_Start()
    {
      BootstrapCommon();

      AreaRegistration.RegisterAllAreas();

      DependencyResolver.SetResolver(IoCHelper.Instance.GetDependencyResolver());

      WebApiConfig.Register(GlobalConfiguration.Configuration);
      FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      BundleConfig.RegisterBundles(BundleTable.Bundles);
      AuthConfig.RegisterAuth();
    }

    protected void Application_Error(object sender, EventArgs e)
    {
      var ex = Server.GetLastError();
      _log.Error(ex.Message, ex);
    }

    public void BootstrapCommon()
    {
      var appDomain = AppDomain.CurrentDomain.BaseDirectory;

      var configPath = Path.Combine(appDomain, "log4net.Release.config");
#if DEBUG
      configPath = Path.Combine(appDomain, "log4net.Debug.config");
#endif

      Bootstraper.Start(new NinjectIoCHelper(), configPath);
      _log = LogHelper.GetLogger(typeof (WebApiApplication));
      AddBindings();
    }

    public void AddBindings()
    {

      IoCHelper.Instance.AddBinding<IGHLocationRepository, EFGHLocationRepository>();
      IoCHelper.Instance.AddBinding<IUserRepository, EFUserRepository>();
      IoCHelper.Instance.AddBinding<ISoundRepository, EFSoundRepository>();
      IoCHelper.Instance.AddBinding<IImageRepository, EFImageRepository>();
      IoCHelper.Instance.AddBinding<ITextRepository, EFTextRepository>();
      IoCHelper.Instance.AddBinding<IVideoRepository, EFVideoRepository>();
      IoCHelper.Instance.AddBinding<IRawSoundRepository, EFRawSoundRepository>();
      IoCHelper.Instance.AddBinding<ISoundService, EFSoundService>();
      IoCHelper.Instance.AddBinding<IRawImageRepository, EFRawImageRepository>();
      IoCHelper.Instance.AddBinding<IImageService, EFImageService>();
      IoCHelper.Instance.AddBinding<IRawTextRepository, EFRawTextRepository>();
      IoCHelper.Instance.AddBinding<ITextService, EFTextService>();
      IoCHelper.Instance.AddBinding<ISessionRepository, EFSessionRepository>();
      IoCHelper.Instance.AddBinding<ISessionService, EFSessionService>();
      IoCHelper.Instance.AddBinding<IGHGeocoderService, DefaultGeocoderService>();
      IoCHelper.Instance.AddBinding<IGHLocationService, EFGHLocationService>();
      IoCHelper.Instance.AddBinding<ISearchService, EFSearchService>();
      IoCHelper.Instance.AddBinding<ITokenRegisterService, EFTokenRegisterService>();
      IoCHelper.Instance.AddBinding<IPotentialUserRepository, EFPotentialUserRepository>();
      IoCHelper.Instance.AddBinding<AUserHelper, DefaultUserHelper>(IoCBindingType.Singleton);
      IoCHelper.Instance.AddBinding<IVideoService, EFVideoService>();
      IoCHelper.Instance.AddBinding<IVideoServiceUrlParser, YouTubeVideoServiceUrlParser>(IoCBindingType.Normal, "YouTube");
      IoCHelper.Instance.AddBinding<ISavedSearchRepository, EFSavedSearchRepository>();
      IoCHelper.Instance.AddBinding<ILastUserSettings, EFLastUserSettings>();
      IoCHelper.Instance.AddBinding<ASMTPEmailService, ArvixeSMTPEmailService>(IoCBindingType.Normal, "Unsecure");
      IoCHelper.Instance.AddBinding<IEmailTemplateBindingService, DefaultEmailTemplateBindingService>();
      IoCHelper.Instance.AddBinding<IEmailTemplateService, DefaultEmailTemplateService>();
      IoCHelper.Instance.AddBinding<ASMTPEmailService, ArvixeSecureSMTPEmailService>(IoCBindingType.Normal, "Secure");
      IoCHelper.Instance.AddBinding<ICacheableService<GHLocationTransmitModel>, EFGHLocationService>();
      IoCHelper.Instance.AddBinding<ITestExceptionService, DefaultTestExceptionService>();

    }

  }
}