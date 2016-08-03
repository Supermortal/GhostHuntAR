using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using GhostHuntAR.Infrastructure.Abstract;
using GhostHuntAR.Infrastructure.Models;
using GhostHuntAR.Web.Filters;
using log4net;
using Supermortal.Common.NonPCL.Helpers.Log;
using Supermortal.Common.PCL.Helpers;
using Supermortal.Common.PCL.Models;

namespace GhostHuntAR.Web.Controllers
{
  [TokenFilter]
  public class SightsAndSoundsController : ApiController
  {

    private static readonly ILog Log = LogHelper.GetLogger
      (typeof(SightsAndSoundsController));

    private readonly ISoundService _ss = (ISoundService) IoCHelper.Instance.GetService(typeof(ISoundService));
    private readonly IImageService _is = (IImageService) IoCHelper.Instance.GetService(typeof(IImageService));
    private readonly ITextService _ts = (ITextService) IoCHelper.Instance.GetService(typeof(ITextService));
    private readonly IVideoService _vs = (IVideoService) IoCHelper.Instance.GetService(typeof(IVideoService));

    [HttpGet]
    public ServerReturnModel<IEnumerable<Sound>> GetPagedSoundsByGHLocationId(int id, int page, int pageSize)
    {
      try
      {
        return new ServerReturnModel<IEnumerable<Sound>>("OK", _ss.FindAllByGHLocationID(id, page, pageSize).ToArray());
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return new ServerReturnModel<IEnumerable<Sound>>("ERROR", ex.Message);
      }
    }

    //[AllowAnonymous]
    //[HttpGet]
    //public ResumingFileStreamResult GetRawSound(long id)
    //{
    //  try
    //  {
    //    return _ss.Get(id);
    //  }
    //  catch (Exception ex)
    //  {
    //    Log.Error(ex.Message, ex);
    //    return null;
    //  }
    //}

    [HttpGet]
    public ServerReturnModel<IEnumerable<Image>> GetPagedImagesByGHLocationId(int id, int page, int pageSize)
    {
      try
      {
        return new ServerReturnModel<IEnumerable<Image>>("SUCCESS", _is.FindAllByGHLocationID(id, page, pageSize).ToList());
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return new ServerReturnModel<IEnumerable<Image>>("ERROR", ex.Message);
      }
    }

    [HttpGet]
    public ServerReturnModel<object> GetRawImage(long id)
    {
      try
      {
        return new ServerReturnModel<object>("SUCCESS", (object)Convert.ToBase64String(_is.GetBytes(id)));
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return new ServerReturnModel<object>("ERROR", ex.Message);
      }
    }

    [HttpGet]
    public ServerReturnModel<IEnumerable<Text>> GetPagedTextsByGHLocationId(int id, int page, int pageSize)
    {
      try
      {
        return new ServerReturnModel<IEnumerable<Text>>("SUCCESS", _ts.FindAllByGHLocationID(id, page, pageSize).ToList());
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return new ServerReturnModel<IEnumerable<Text>>("ERROR", ex.Message);
      }
    }

    [HttpGet]
    public ServerReturnModel<object> GetRawText(long id)
    {
      try
      {
        return new ServerReturnModel<object>("SUCCESS", (object)_ts.Get(id));
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return new ServerReturnModel<object>("ERROR", ex.Message);
      }
    }

    [HttpGet]
    public ServerReturnModel<IEnumerable<Video>> GetPagedVideosByGHLocationId(int id, int page, int pageSize)
    {
      try
      {
        return new ServerReturnModel<IEnumerable<Video>>("SUCCESS", _vs.FindAllByGHLocationID(id, page, pageSize).ToList());
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return new ServerReturnModel<IEnumerable<Video>>("ERROR", ex.Message);
      }
    }

    [HttpGet]
    public ServerReturnModel<object> GetRawVideo(long id)
    {
      try
      {
        return new ServerReturnModel<object>("SUCCESS", (object)_vs.Get(id));
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return new ServerReturnModel<object>("ERROR", ex.Message);
      }
    }

  }
}
