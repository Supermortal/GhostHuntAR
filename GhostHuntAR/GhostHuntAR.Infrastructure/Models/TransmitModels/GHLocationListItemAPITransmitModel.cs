namespace GhostHuntAR.Infrastructure.Models.TransmitModels
{
  public class GHLocationListItemAPITransmitModel : GHLocationListItemTransmitModel
  {

    public string DateCreatedISO
    {
      get { return DateCreated.ToString("o"); }
    }

    public GHLocationListItemAPITransmitModel()
    {     
    }

    public GHLocationListItemAPITransmitModel(GHLocation location)
      : base(location)
    {
    }

  }
}