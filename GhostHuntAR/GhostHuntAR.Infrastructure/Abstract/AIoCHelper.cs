using System.Web.Mvc;
using GhostHuntAR.Infrastructure.Helpers;
using log4net;
using Ninject;

namespace GhostHuntAR.Infrastructure.Abstract
{
  public abstract class AIoCHelper
  {

    private static readonly ILog Log = LogHelper.GetLogger(typeof(AIoCHelper));

    public abstract IKernel Kernel { get; protected set; }
    public abstract IDependencyResolver DependencyResolver { get; protected set; }

    public abstract T GetNamedBinding<T>(string name);

  }
}