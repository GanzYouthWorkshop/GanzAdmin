using System;
using System.Collections.Generic;
using System.Text;

namespace GanzAdmin.Utils
{
    public static class Extensions
    {
        public static bool ContainsAny<T>(this List<T> haystack, List<T> needles)
        {
            bool result = false;

            foreach(T needle in needles)
            {
                if(haystack != null && haystack.Contains(needle))
                {
                    result = true;
                }
            }

            return result;
        }

        public static bool ContainsAll<T>(this List<T> haystack, List<T> needles)
        {
            bool result = true;

            foreach (T needle in needles)
            {
                if (haystack != null && !haystack.Contains(needle))
                {
                    result = false;
                }
            }

            return result;
        }

    }
}
