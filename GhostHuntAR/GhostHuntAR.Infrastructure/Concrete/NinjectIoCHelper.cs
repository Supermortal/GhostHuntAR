using System;
using System.Web.Mvc;
using GhostHuntAR.Infrastructure.Abstract;
using GhostHuntAR.Infrastructure.Helpers;
using log4net;
using NamespaceMedia.Common.Abstract;
using NamespaceMedia.Common.Helpers.Log;
using Ninject;
using Ninject.Parameters;

namespace GhostHuntAR.Infrastructure.Concrete
{
  public class NinjectIoCHelper : IIoCHelper
  {
    private static readonly ILog Log = LogHelper.GetLogger(typeof(NinjectIoCHelper));

    //private IKernel _kernel = null;
    //public override IKernel Kernel
    //{
    //  get { return _kernel ?? (_kernel = new StandardKernel()); }
    //  protected set { _kernel = value; }
    //}

    //private IDependencyResolver _dependencyResolver = null;
    //public override IDependencyResolver DependencyResolver
    //{
    //  get { return _dependencyResolver ?? (_dependencyResolver = new NinjectDependencyResolver()); }
    //  protected set { _dependencyResolver = value; }
    //}

    //public override T GetNamedBinding<T>(string name)
    //{
    //  return GetNamedBinding<T>(name, null);
    //}

    //public T GetNamedBinding<T>(string name, IParameter parameters)
    //{
    //  try
    //  {
    //    return (T)Kernel.TryGet(typeof(T), name, parameters);
    //  }
    //  catch (Exception ex)
    //  {
    //    Log.Error(ex.Message, ex);
    //    return default(T);
    //  }
    //}

    public IDependencyResolver GetDependencyResolver()
    {
      throw new NotImplementedException();
    }

    public T GetNamedBinding<T>(string name)
    {
      throw new NotImplementedException();
    }

    public void AddBinding<T1, T2>() where T2 : T1
    {
      throw new NotImplementedException();
    }

    public void AddBinding<T1, T2>(string name) where T2 : T1
    {
      throw new NotImplementedException();
    }

    public object GetService(Type serviceType)
    {
      throw new NotImplementedException();
    }
  }
}