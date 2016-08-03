using System.ComponentModel.DataAnnotations;

namespace GhostHuntAR.Infrastructure.Models
{
  public class RawText
  {
    [Key]
    //[DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long RawTextID { get; set; }
    public string Body { get; set; }
  }
}