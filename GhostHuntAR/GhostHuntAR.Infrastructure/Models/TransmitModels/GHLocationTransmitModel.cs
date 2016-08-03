using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Supermortal.Common.PCL.Abstract;

namespace GhostHuntAR.Infrastructure.Models.TransmitModels
{
  public class GHLocationTransmitModel : ICacheable
  {

    public GHLocationTransmitModel()
    {
      
    }

    public GHLocationTransmitModel(GHLocation location)
    {
      GHLocationID = location.GHLocationID;
      DateCreated = location.DateCreated.ToString("o");
      Flags = location.Flags;
      Title = location.Title;
      Text = location.Text;
      Altitude = location.Altitude;
      Latitude = location.Latitude;
      Longitude = location.Longitude;
      CreatedByUserID = location.CreatedByUserID;
      SoundsCount = location.Sounds.Count();
      ImagesCount = location.Images.Count();
      VideosCount = location.Videos.Count();
      TextsCount = location.Texts.Count();
      DateLastModified = location.DateLastModified.ToString("o");
    }

    public long GHLocationID { get; set; }
    public string DateCreated { get; set; }
    public string DateLastModified { get; set; }
    public ICollection<Flag.Flag> Flags { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public double? Altitude { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public long CreatedByUserID { get; set; }
    //public List<Sound> Sounds { get; set; }
    //public List<Image> Images { get; set; }
    //public List<Video> Videos { get; set; }
    //public List<Text> Texts { get; set; }
    public int SoundsCount { get; set; }
    public int ImagesCount { get; set; }
    public int VideosCount { get; set; }
    public int TextsCount { get; set; }

    public DateTime GetDateLastModified()
    {
      return DateTime.Parse(DateLastModified, null, DateTimeStyles.AdjustToUniversal);
    }

    public void SetDateLastModified(DateTime dateLastModified)
    {
      DateLastModified = dateLastModified.ToString("o");
    }

  }
}