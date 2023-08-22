using GanzAdmin.Database.Models;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GanzAdmin.Database
{
    public static class GanzDbExtensions
    {
        public static List<IEntity> ToEntities<T>(this List<T> items) where T : IEntity
        {
            List<IEntity> result = new List<IEntity>();
            foreach(IEntity item in items)
            {
                result.Add(item);
            }
            return result;
        }

        public static List<T> FromEntities<T>(this List<IEntity> items) where T : IEntity
        {
            List<T> result = new List<T>();
            foreach (IEntity item in items)
            {
                result.Add((T)item);
            }
            return result;
        }

        public static bool IsEqual(this IEntity obj1, object obj2)
        {
            if(obj1 != null && obj2 != null)
            {
                return obj1.GetType().Equals(obj2.GetType()) && obj1.Id == (obj2 as IEntity).Id;
            }
            return false;          
        }

        public static string ToDisplayUrl(this IEntity entity)
        {
            char[] fromReplace = { 'á', 'é', 'í', 'ö', 'ü', 'ó', 'ő', 'ú', 'ű', 'ä', '/', ' ' };
            char[] toReplace =   { 'a', 'e', 'i', 'o', 'u', 'o', 'o', 'u', 'u', 'a', '-', '-' };

			string displayUrl = entity.DisplayValue.ToLower();
            displayUrl = Regex.Replace(displayUrl, @"[^\u0000-\u007F]+", string.Empty).Trim();
            for (int i = 0; i < fromReplace.Length; i++)
            {
                displayUrl = displayUrl.Replace(fromReplace[i], toReplace[i]);
			}
			displayUrl = Regex.Replace(displayUrl, @"[^\u0000-\u007F]+", string.Empty).Trim();

            return $"{entity.Id}-{displayUrl}";
		}

		public static int FromDisplayUrl(this string displayUrl)
        {
            return int.Parse(displayUrl.Split('-').First());
        }

	}
}
