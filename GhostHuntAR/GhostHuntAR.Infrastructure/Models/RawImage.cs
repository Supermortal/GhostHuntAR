using System.ComponentModel.DataAnnotations;

namespace GhostHuntAR.Infrastructure.Models
{
  public class RawImage
  {
    [Key]
    //[DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long RawImageID { get; set; }
    public byte[] Data { get; set; }
    public string MIMEType { get; set; }
    public string FileName { get; set; }
  }
}