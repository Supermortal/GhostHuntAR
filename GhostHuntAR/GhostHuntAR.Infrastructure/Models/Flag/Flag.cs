using System.ComponentModel.DataAnnotations;

namespace GhostHuntAR.Infrastructure.Models.Flag
{
  public class Flag
  {
      [Key]
      public long FlagID { get; set; }
      public string FlagType { get; set; }
      public string OtherField { get; set; }
  }
}