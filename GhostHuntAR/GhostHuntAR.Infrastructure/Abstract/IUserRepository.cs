using System.Linq;
using GhostHuntAR.Infrastructure.Models;
using GhostHuntAR.Infrastructure.Models.Enums;

namespace GhostHuntAR.Infrastructure.Abstract
{
  public interface IUserRepository
  {
    IQueryable<UserProfile> GetAll();
    UserProfile Find(long id);
    void Delete(UserProfile user);
    void Delete(long id);
    long Save(UserProfile user);
    void Update(UserProfile user);
    void SetContext(object context);
    IQueryable<UserProfile> FindByUserName(string userName, bool useContains);
    IQueryable<UserProfile> FindByUserName(string userName);
    IQueryable<UserProfile> FindByRole(string role);
    IQueryable<UserProfile> FindByEmail(string email);
    void Dispose();
    object GetContext();
    void DeleteSearch(UserProfile user);
    void SaveSearch(long userId, SavedSearch search);
    void ChangeLastUserSettingsType(long userId, LastUserSettingsTypes type);
    void ChangeLastUserSettingsType(long userId);
  }
}
