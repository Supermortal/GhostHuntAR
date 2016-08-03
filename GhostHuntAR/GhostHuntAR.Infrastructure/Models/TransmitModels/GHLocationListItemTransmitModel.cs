using System;
using System.Collections.Generic;
using System.Linq;

namespace GhostHuntAR.Infrastructure.Models.TransmitModels
{
  public class GHLocationListItemTransmitModel
  {

    public GHLocationListItemTransmitModel()
    {

    }

    public GHLocationListItemTransmitModel(GHLocation location)
    {
      GHLocationID = location.GHLocationID;
      DateCreated = location.DateCreated;
      Flags = location.Flags;
      Title = location.Title;
      Text = location.Text;
      Altitude = location.Altitude;
      Latitude = location.Latitude;
      Longitude = location.Longitude;
      CreatedByUserID = location.CreatedByUserID;
      ImageCaption = location.ImageCaption;
      AddressLine = location.AddressLine;
      City = location.City;
      State = location.State;
      ZipPostalCode = location.ZipPostalCode;

      SoundsCount = location.Sounds.Count();
      ImagesCount = location.Images.Count();
      TextsCount = location.Texts.Count();
    }

    public long GHLocationID { get; set; }
    public DateTime DateCreated { get; set; }
    public ICollection<Flag.Flag> Flags { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public double? Altitude { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public long CreatedByUserID { get; set; }
    public string ImageCaption { get; set; }
    public int SoundsCount { get; set; }
    public int ImagesCount { get; set; }
    public int TextsCount { get; set; }
    public string AddressLine { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipPostalCode { get; set; }

  }
}