using System.ComponentModel.DataAnnotations;

namespace GhostHuntAR.Infrastructure.Models
{
  public class RawSound
  {
    [Key]
    //[DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long RawSoundID { get; set; }
    public byte[] Data { get; set; }
    public string MIMEType { get; set; }
    public string FileName { get; set; }
  }
}