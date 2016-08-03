using GhostHuntAR.Infrastructure.Models;

namespace GhostHuntAR.Infrastructure.Abstract
{
  public interface ISessionService
  {
    Session CreateSession(string userName);
    UserProfile GetUser(string token);
    bool? ValidateToken(string token);
  }
}
