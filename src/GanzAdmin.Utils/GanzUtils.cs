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

        public static string CreateDataUrl(string content, string mime)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(content);
            string base64 = Convert.ToBase64String(plainTextBytes);

            return $"data:{mime};base64,{base64}";

        }
    }
}
