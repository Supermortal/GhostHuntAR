using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GhostHuntAR.Infrastructure.Models
{
  public class Text
  {

    public Text()
    {
        Flags = new List<Flag.Flag>();
    }

    [Key]
    public long TextID { get; set; }
    //[Column(TypeName = "VARCHAR")]
    [StringLength(450)]
    public string Title { get; set; }
    //[Column(TypeName = "VARCHAR")]
    [StringLength(450)]
    public string UserName { get; set; }
    public string Url { get; set; }
    public virtual long GHLocationID { get; set; }
    [HiddenInput(DisplayValue = false)]
    public virtual ICollection<Flag.Flag> Flags { get; private set; }
  }
}