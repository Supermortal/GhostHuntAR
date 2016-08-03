using System;
using System.Security.Cryptography;
using System.Text;

namespace GhostHuntAR.Infrastructure.Helpers
{
    public class Security
    {

        private static readonly log4net.ILog Log = LogHelper.GetLogger
            (typeof(Security));

        public static string Hash(string input)
        {
            try
            {
                var algorithm = HashAlgorithm.Create("SHA256");

                Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

                var sb = new StringBuilder();

                foreach (var t in hashedBytes)
                {
                    sb.Append(String.Format("{0:x2}", t));
                }

                return sb.ToString();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

    }
}