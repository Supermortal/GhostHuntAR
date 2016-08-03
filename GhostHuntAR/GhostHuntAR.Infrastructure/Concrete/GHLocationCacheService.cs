using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GhostHuntAR.Infrastructure.Models;
using NamespaceMedia.Common.Abstract;
using NamespaceMedia.Common.Helpers.Log;

namespace GhostHuntAR.Infrastructure.Concrete
{
  public class GHLocationCacheService : ICacheService<GHLocation>
  {

    private static readonly log4net.ILog Log = LogHelper.GetLogger
        (typeof(GHLocationCacheService));

    private ConcurrentDictionary<string, GHLocation> Cache { get; set; }
    private readonly ICacheableService<GHLocation> _cs; 

    public GHLocationCacheService(ICacheableService<GHLocation> cs)
    {
      _cs = cs;
      Cache = new ConcurrentDictionary<string, GHLocation>();
    }

    public void Dispose()
    {
      try
      {
        Cache.Clear();
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

    public GHLocation GetCacheable(string id, DateTime dateLastModified)
    {
      try
      {
        if (!Cache.ContainsKey(id))
          Cache[id] = _cs.GetCacheable(id);

        return Cache[id];
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public GHLocation GetCacheableForEdit(string id)
    {
      try
      {
        return Cache.ContainsKey(id) ? Cache[id] : null;
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public void SetCacheable(string key, GHLocation obj)
    {
      try
      {
        Cache[key] = obj;
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

    public bool ContainsKey(string key)
    {
      try
      {
        return Cache.ContainsKey(key);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return false;
      }
    }

  }
}