using System;
using GhostHuntAR.Infrastructure.Abstract;
using GhostHuntAR.Infrastructure.Models.TokenRegister;
using Supermortal.Common.NonPCL.Helpers.Log;
using Supermortal.Common.PCL.Helpers;

namespace GhostHuntAR.Infrastructure.Concrete
{
  public class EFTokenRegisterService : ITokenRegisterService
  {

    private static readonly log4net.ILog Log = LogHelper.GetLogger
        (typeof(EFTokenRegisterService));

    private IPotentialUserRepository _pur;

    public EFTokenRegisterService(IPotentialUserRepository pur)
    {
      _pur = pur;
    }

    public string GenerateTokenForPotentialUser(PotentialUser pu)
    {
      try
      {
        return Crypto.Hash(pu.Name + pu.Email);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    public void GetAllPotentialUsersWithCount(ref PotentialUserParcel pup, int page, int pageSize, bool? signedUp)
    {
      try
      {
        _pur.GetAllWithCount(ref pup, page, pageSize, signedUp);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        throw ex;
      }
    }

    public PotentialUser FindPotentialUser(long id)
    {
      try
      {
        return _pur.Find(id);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        throw ex;
      }
    }

    public PotentialUser FindPotentialUserFromToken(string token)
    {
      try
      {
        return _pur.FindByToken(token);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        throw ex;
      }
    }

    public void SavePotentialUser(PotentialUser pu)
    {
      try
      {
        pu.Token = GenerateTokenForPotentialUser(pu);

        _pur.Save(pu);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        throw ex;
      }
    }

    public void UpdatePotentialUser(PotentialUser pu)
    {
      try
      {
        pu.Token = GenerateTokenForPotentialUser(pu);

        _pur.Update(pu);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        throw ex;
      }
    }

    public void DeletePotentialUser(PotentialUser pu)
    {
      try
      {
        _pur.Delete(pu);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        throw ex;
      }
    }

    public void DeletePotentialUser(long id)
    {
      try
      {
        _pur.Delete(id);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        throw ex;
      }
    }

  }
}