using System;
using System.Security.Cryptography;
using System.Text;

namespace GanzAdmin.Utils
{
    public class GanzUtils
    {
        public static string Sha256(string text)
        {
            string result;

            using (SHA256 mySHA256 = SHA256.Create())
            {
                result = mySHA256.ComputeHash(Encoding.UTF8.GetBytes(text)).ToString();
            }

            return result;
        }
    }
}
