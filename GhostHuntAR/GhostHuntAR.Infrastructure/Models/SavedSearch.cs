using System;
using System.ComponentModel.DataAnnotations;

namespace GhostHuntAR.Infrastructure.Models
{
  public class SavedSearch
  {

    public SavedSearch()
    {
      SavedSearchID = Guid.NewGuid().ToString();
    }

    [Key]
    public string SavedSearchID { get; set; }  
    public string Keywords { get; set; }
    public string Address { get; set; }
    public string DistanceType { get; set; }
    public string Radius { get; set; }
    public string City { get; set; }
    public string State { get; set; } 
    public string Zip { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public virtual int UserId { get; set; }
  }
}