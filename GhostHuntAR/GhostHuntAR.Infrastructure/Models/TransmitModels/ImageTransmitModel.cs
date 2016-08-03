using System.Collections.Generic;

namespace GhostHuntAR.Infrastructure.Models.TransmitModels
{
    public class ImageTransmitModel
    {

        public ImageTransmitModel(Image i)
        {
            ImageID = i.ImageID;
            GHLocationID = i.GHLocationID;
            Caption = i.Caption;
            UserName = i.UserName;
            Url = i.Url;
            Flags = i.Flags;
        }

        public long ImageID { get; set; }
        public virtual long GHLocationID { get; set; }
        public string Caption { get; set; }
        public string UserName { get; set; }
        public string Url { get; set; }
        public ICollection<Flag.Flag> Flags { get; set; }
    }
}