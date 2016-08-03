using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Supermortal.Common.PCL.Abstract;

namespace GhostHuntAR.Infrastructure.Models
{
  public class GHLocation : ICacheable
  {

    public GHLocation()
    {
      Users = new List<UserProfile>();
      Sounds = new List<Sound>();
      Videos = new List<Video>();
      Texts = new List<Text>();
      Images = new List<Image>();
      Flags = new List<Flag.Flag>();
      DateCreated = DateLastModified = DateTime.UtcNow;
    }

    [Key]
    [HiddenInput(DisplayValue = false)]
    public long GHLocationID { get; set; }

    [HiddenInput(DisplayValue = false)]
    public DateTime DateCreated { get; set; }

    [HiddenInput(DisplayValue = false)]
    public DateTime DateLastModified { get; set; }

    [HiddenInput(DisplayValue = false)]
    public virtual ICollection<Flag.Flag> Flags { get; private set; }

    //[Column(TypeName = "VARCHAR")]
    [StringLength(450)]
    public string Title { get; set; }

    public string Text { get; set; }
    public double? Altitude { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }

    //[Column(TypeName = "VARCHAR")]
    [StringLength(450)]
    [DisplayName("Address Line")]
    public string AddressLine { get; set; }

    //[Column(TypeName = "VARCHAR")]
    [StringLength(450)]
    public string City { get; set; }

    //[Column(TypeName = "VARCHAR")]
    [StringLength(450)]
    public string State { get; set; }

    [DisplayName("Zip/Postal Code")]
    public string ZipPostalCode { get; set; }

    [HiddenInput(DisplayValue = false)]
    public long CreatedByUserID { get; set; }

    [HiddenInput(DisplayValue = false)]
    public virtual ICollection<UserProfile> Users { get; set; }
    public virtual ICollection<Sound> Sounds { get; private set; }
    public virtual ICollection<Image> Images { get; private set; }
    public virtual ICollection<Video> Videos { get; private set; }
    public virtual ICollection<Text> Texts { get; private set; }

    [DisplayName("Image Caption")]
    public string ImageCaption { get; set; }
    public byte[] Image { get; set; }

    public void SetAllEditedProperties(GHLocation location)
    {
      Title = location.Title;
      Text = location.Text;
      Altitude = location.Altitude;
      Latitude = location.Latitude;
      Longitude = location.Longitude;
      AddressLine = location.AddressLine;
      City = location.City;
      State = location.State;
      ZipPostalCode = location.ZipPostalCode;
      ImageCaption = location.ImageCaption;
    }

    public DateTime GetDateLastModified()
    {
      return DateLastModified;
    }

    public void SetDateLastModified(DateTime dateLastModified)
    {
      DateLastModified = dateLastModified;
    }

  }
}