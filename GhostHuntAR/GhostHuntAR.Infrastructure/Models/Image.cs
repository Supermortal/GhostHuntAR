using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GhostHuntAR.Infrastructure.Models
{
  public class Image
  {

    public Image()
    {
        Flags = new List<Flag.Flag>();
    }

    [Key]
    public long ImageID { get; set; }
    public virtual long GHLocationID { get; set; }
    public byte[] Thumbnail { get; set; }
    //[Column(TypeName = "VARCHAR")]
    [StringLength(450)]
    public string Caption { get; set; }
    //[Column(TypeName = "VARCHAR")]
    [StringLength(450)]
    public string UserName { get; set; }
    public string Url { get; set; }
    [HiddenInput(DisplayValue = false)]
    public virtual ICollection<Flag.Flag> Flags { get; private set; }
  }
}