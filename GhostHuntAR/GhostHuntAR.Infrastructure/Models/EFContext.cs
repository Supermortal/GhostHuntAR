using System.Data.Entity;
using GhostHuntAR.Infrastructure.Models.TokenRegister;

namespace GhostHuntAR.Infrastructure.Models
{
  public class EFContext : DbContext
  {

    public EFContext()
      : base("name=EFContext")
    {
    }

    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<GHLocation> GHLocations { get; set; }
    public DbSet<Sound> Sounds { get; set; }
    public DbSet<Video> Videos { get; set; }
    public DbSet<Text> Texts { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<RawSound> RawSounds { get; set; }
    public DbSet<RawText> RawTexts { get; set; }
    public DbSet<RawImage> RawImages { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Flag.Flag> Flags { get; set; }
    public DbSet<PotentialUser> PotentialUsers { get; set; }
    public DbSet<SavedSearch> SavedSearches { get; set; }
    public DbSet<LastUserSettings> LastUserSettings { get; set; }

  }

}