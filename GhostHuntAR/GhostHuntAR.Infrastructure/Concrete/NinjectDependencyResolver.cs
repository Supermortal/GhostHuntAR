using System;
using System.Collections.Generic;
using System.Web.Mvc;
using GhostHuntAR.Infrastructure.Abstract;
using NamespaceMedia.Common.Abstract.Email;
using NamespaceMedia.Common.Concrete.Email.SMTP;
using Ninject;

namespace GhostHuntAR.Infrastructure.Concrete
{
  public class NinjectDependencyResolver : IDependencyResolver
  {

    private readonly IKernel _kernel = new StandardKernel();

    public NinjectDependencyResolver()
    {
      AddBindings();
    }

    public object GetService(Type serviceType)
    {
      return _kernel.TryGet(serviceType);
    }

    public IEnumerable<object> GetServices(Type serviceType)
    {
      return _kernel.GetAll(serviceType);
    }

    private void AddBindings()
    {

      _kernel.Bind<IGHLocationRepository>().To<EFGHLocationRepository>();
      _kernel.Bind<IUserRepository>().To<EFUserRepository>();
      _kernel.Bind<ISoundRepository>().To<EFSoundRepository>();
      _kernel.Bind<IImageRepository>().To<EFImageRepository>();
      _kernel.Bind<ITextRepository>().To<EFTextRepository>();
      _kernel.Bind<IVideoRepository>().To<EFVideoRepository>();
      _kernel.Bind<IRawSoundRepository>().To<EFRawSoundRepository>();
      _kernel.Bind<ISoundService>().To<EFSoundService>();
      _kernel.Bind<IRawImageRepository>().To<EFRawImageRepository>();
      _kernel.Bind<IImageService>().To<EFImageService>();
      _kernel.Bind<IRawTextRepository>().To<EFRawTextRepository>();
      _kernel.Bind<ITextService>().To<EFTextService>();
      _kernel.Bind<ISessionRepository>().To<EFSessionRepository>();
      _kernel.Bind<ISessionService>().To<EFSessionService>();
      _kernel.Bind<IGeocoderService>().To<GoogleGeocoderService>();
      _kernel.Bind<IGHLocationService>().To<EFGHLocationService>();
      _kernel.Bind<ISearchService>().To<EFSearchService>();
      _kernel.Bind<ITokenRegisterService>().To<EFTokenRegisterService>();
      _kernel.Bind<IPotentialUserRepository>().To<EFPotentialUserRepository>();
      _kernel.Bind<AUserHelper>().To<DefaultUserHelper>().InSingletonScope();
      _kernel.Bind<IVideoService>().To<EFVideoService>();
      _kernel.Bind<IVideoServiceUrlParser>().To<YouTubeVideoServiceUrlParser>().Named("YouTube");
      _kernel.Bind<ISavedSearchRepository>().To<EFSavedSearchRepository>();
      _kernel.Bind<ILastUserSettings>().To<EFLastUserSettings>();
      _kernel.Bind<ASMTPEmailService>().To<ArvixeSMTPEmailService>();

    }

  }
}