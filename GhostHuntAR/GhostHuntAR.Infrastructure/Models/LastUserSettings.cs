using System;
using System.ComponentModel.DataAnnotations;

namespace GhostHuntAR.Infrastructure.Models
{
  public class LastUserSettings
  {

    public LastUserSettings()
    {
      LastUserSettingsID = Guid.NewGuid().ToString();
    }

    [Key]
    public string LastUserSettingsID { get; set; }
    public virtual int UserId { get; set; }
    public string LastUserSettingsType { get; set; }
    public virtual SavedSearch SavedSearch { get; set; }

  }
}