using System;
using System.IO;
using System.Linq;
using System.Web;
using GhostHuntAR.Infrastructure.Abstract;
using GhostHuntAR.Infrastructure.Models;
using Supermortal.Common.NonPCL.Helpers.Log;
using VikingErik.Mvc.ResumingActionResults;

namespace GhostHuntAR.Infrastructure.Concrete
{
  public class EFSoundService : ISoundService
  {

    private static readonly log4net.ILog Log = LogHelper.GetLogger
        (typeof(EFSoundService));

    private IRawSoundRepository _rsr;
    private ISoundRepository _sr;

    public EFSoundService(IRawSoundRepository rsr, ISoundRepository sr)
    {
      _rsr = rsr;
      _sr = sr;
    }

    public ResumingFileStreamResult Get(long id)
    {
      try
      {
        var rs = _rsr.Find(id);
        return new ResumingFileStreamResult(new MemoryStream(rs.Data), rs.MIMEType);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public Sound GetSound(long id)
    {
      try
      {
        return _sr.Find(id);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public void Save(long ghLocationId, HttpPostedFileBase sound, string soundDescription, string userName)
    {
      try
      {

        var arr = new byte[sound.ContentLength];

        sound.InputStream.Read(arr, 0, sound.ContentLength);

        var rawSound = new RawSound();
        rawSound.Data = arr;
        rawSound.FileName = sound.FileName;
        rawSound.MIMEType = sound.ContentType;

        var s = new Sound();
        s.GHLocationID = ghLocationId;
        s.Description = soundDescription;
        s.UserName = userName;
        _sr.Insert(s);

        rawSound.RawSoundID = s.SoundID;
        _rsr.Insert(rawSound);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

    public void Update(long soundId, HttpPostedFileBase sound, string soundDescription)
    {
      try
      {

        var s = _sr.Find(soundId);
        if (s == null)
          return;
        s.Description = soundDescription;
        _sr.Update(s);

        if (sound != null)
        {
          var arr = new byte[sound.ContentLength];
          sound.InputStream.Read(arr, 0, arr.Length);

          var rawSound = _rsr.Find(soundId);
          if (rawSound == null)
            return;
          rawSound.Data = arr;
          rawSound.FileName = sound.FileName;
          rawSound.MIMEType = sound.ContentType;
          _rsr.Update(rawSound);
        }
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

    public void Delete(long soundId)
    {
      try
      {
        _sr.Delete(soundId);
        _rsr.Delete(soundId);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

    public IQueryable<Sound> FindAllByUserName(string userName)
    {
      try
      {
        return _sr.FindAllByUserName(userName);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public IQueryable<Sound> FindAllByUserName(string userName, int page, int pageSize)
    {
      try
      {
        return _sr.FindAllByUserName(userName, page, pageSize);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public IQueryable<Sound> FindAllByGHLocationID(long id)
    {
      try
      {
        return _sr.FindAllByGHLocationID(id);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public IQueryable<Sound> FindAllByGHLocationID(long id, int page, int pageSize)
    {
      try
      {
        return _sr.FindAllByGHLocationID(id, page, pageSize);
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
        _sr.Dispose();
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

  }
}