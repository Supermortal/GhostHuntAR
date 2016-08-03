using GhostHuntAR.Infrastructure.Models.TokenRegister;

namespace GhostHuntAR.Infrastructure.Abstract
{
  public interface ITokenRegisterService
  {
    string GenerateTokenForPotentialUser(PotentialUser pu);
    void GetAllPotentialUsersWithCount(ref PotentialUserParcel pup, int page, int pageSize, bool? signedUp);
    PotentialUser FindPotentialUser(long id);
    PotentialUser FindPotentialUserFromToken(string token);
    void SavePotentialUser(PotentialUser pu);
    void UpdatePotentialUser(PotentialUser pu);
    void DeletePotentialUser(PotentialUser pu);
    void DeletePotentialUser(long id);
  }
}
