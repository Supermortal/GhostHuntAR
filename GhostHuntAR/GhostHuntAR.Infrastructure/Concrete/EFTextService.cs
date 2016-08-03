using System;
using System.Linq;
using GhostHuntAR.Infrastructure.Abstract;
using GhostHuntAR.Infrastructure.Models;
using Supermortal.Common.NonPCL.Helpers.Log;

namespace GhostHuntAR.Infrastructure.Concrete
{
  public class EFTextService : ITextService
  {

    private static readonly log4net.ILog Log = LogHelper.GetLogger
        (typeof(EFTextService));

    private ITextRepository _tr;
    private IRawTextRepository _rtr;

    public EFTextService(ITextRepository tr, IRawTextRepository rtr)
    {
      _tr = tr;
      _rtr = rtr;
    }

    public string Get(long id)
    {
      try
      {
        var rt = _rtr.Find(id);

        return rt != null ? rt.Body : null;
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public Text GetText(long id)
    {
      try
      {
        return _tr.Find(id);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public void Save(long ghLocationId, string title, string body, string userName)
    {
      try
      {

        var t = new Text();
        t.Title = title;
        t.UserName = userName;
        t.GHLocationID = ghLocationId;

        _tr.Insert(t);

        var rt = new RawText();
        rt.Body = body;
        rt.RawTextID = t.TextID;

        _rtr.Insert(rt);

      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

    public void Update(long textId, string title, string body)
    {
      try
      {

        var t = _tr.Find(textId);
        if (t == null)
          return;
        t.Title = title;

        _tr.Update(t);

        var rt = _rtr.Find(textId);
        if (rt == null)
          return;
        rt.Body = body;

        _rtr.Update(rt);

      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

    public void Update(long textId, string title)
    {
      try
      {

        var t = _tr.Find(textId);
        if (t == null)
          return;
        t.Title = title;

        _tr.Update(t);

      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

    public void Delete(long textId)
    {
      try
      {
        _tr.Delete(textId);
        _rtr.Delete(textId);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

    public IQueryable<Text> FindAllByUserName(string userName)
    {
      try
      {
        return _tr.FindAllByUserName(userName);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public IQueryable<Text> FindAllByUserName(string userName, int page, int pageSize)
    {
      try
      {
        return _tr.FindAllByUserName(userName, page, pageSize);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public IQueryable<Text> FindAllByGHLocationID(long id)
    {
      try
      {
        return _tr.FindAllByGHLocationID(id);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public IQueryable<Text> FindAllByGHLocationID(long id, int page, int pageSize)
    {
      try
      {
        return _tr.FindAllByGHLocationID(id, page, pageSize);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public void Dispose()
    {
      try
      {
        _tr.Dispose();
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

  }
}