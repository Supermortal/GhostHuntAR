using GhostHuntAR.Infrastructure.Models.TokenRegister;

namespace GhostHuntAR.Infrastructure.Abstract
{
  public interface IPotentialUserRepository
  {
    void GetAllWithCount(ref PotentialUserParcel pup, int page, int pageSize, bool? signedUp);
    PotentialUser Find(long id);
    PotentialUser FindByToken(string token);
    void Save(PotentialUser pu);
    void Update(PotentialUser pu);
    void Delete(PotentialUser pu);
    void Delete(long id);
  }
}
