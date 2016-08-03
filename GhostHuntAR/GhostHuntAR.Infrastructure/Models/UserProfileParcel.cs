using System.Linq;

namespace GhostHuntAR.Infrastructure.Models
{
    public class UserProfileParcel
    {
        public UserProfile User { get; set; }
        public IQueryable<Image> Images { get; set; }
        public IQueryable<Sound> Sounds { get; set; }
        public IQueryable<Video> Videos { get; set; }
        public IQueryable<Text> Texts { get; set; } 
    }
}