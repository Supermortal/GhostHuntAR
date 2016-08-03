using System;

namespace GhostHuntAR.Infrastructure.Helpers
{
    public class MilesKilometersConverter
    {

        private static readonly log4net.ILog Log = LogHelper.GetLogger
            (typeof(MilesKilometersConverter));

        public static double MilesToKilometers(double miles)
        {
            try
            {
                return miles * 1.609344;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public static double KilometersToMiles(double kilometers)
        {
            try
            {
                return kilometers * 0.621371192;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw ex;
            }
        }

    }
}