using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace GanzAdmin.Utils
{
    public class GanzUtils
    {
        public static string Sha256(string text)
        {
            string result = null;

            if (text != null)
            {
                using (SHA256 mySHA256 = SHA256.Create())
                {
                    result = BitConverter.ToString(mySHA256.ComputeHash(Encoding.UTF8.GetBytes(text)));
                }
            }

            return result;
        }

        public static string CreateDataUrl(string content, string mime)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(content);
            string base64 = Convert.ToBase64String(plainTextBytes);

            return $"data:{mime};base64,{base64}";
        }

        public static string ProperPathCombine(char separator, params string[] paths)
        {
            List<string> folders = new List<string>();

            foreach (string path in paths)
            {
                if (path != null)
                {
                    folders.AddRange(path.Split('\\', '/'));
                }
            }

            folders = folders.Where(s => !String.IsNullOrEmpty(s)).ToList();

            return String.Join(separator, folders);
        }
    }
}
