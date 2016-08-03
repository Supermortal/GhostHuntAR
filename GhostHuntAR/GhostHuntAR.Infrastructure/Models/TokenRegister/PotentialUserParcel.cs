using System.Linq;

namespace GhostHuntAR.Infrastructure.Models.TokenRegister
{
    public class PotentialUserParcel
    {
        public IQueryable<PotentialUser> PotentialUsers { get; set; }
        public int? Count { get; set; }
    }
}