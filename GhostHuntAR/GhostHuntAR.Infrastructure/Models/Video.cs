using System.ComponentModel.DataAnnotations;

namespace GhostHuntAR.Infrastructure.Models
{
  public class Video
  {
    [Key]
    public long VideoID { get; set; }
    //[Column(TypeName = "VARCHAR")]
    [StringLength(450)]
    public string UserName { get; set; }
    public string Url { get; set; }
    public string Description { get; set; }
    public virtual long GHLocationID { get; set; }
    [Required]
    public string Type { get; set; }
  }
}