using System;
using System.Collections.Generic;
using System.Text;

namespace GanzNet.Common
{
    public class UrlUtils
    {
        public static string ToAnchor(string text)
        {
            text = text.ToLowerInvariant();
            text = text.Replace(' ', '-');
            text = text.Replace('\t', '-');
            text = text.Replace('á', 'a');
            text = text.Replace('é', 'e');
            text = text.Replace('ö', 'o');
            text = text.Replace('ő', 'o');
            text = text.Replace('ü', 'u');
            text = text.Replace('ű', 'u');
            text = text.Replace('ú', 'u');
            text = text.Replace('ó', 'o');
            text = text.Replace('í', 'i');

            return text;
        }
    }
}
