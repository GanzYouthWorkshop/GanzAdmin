using GanzAdmin.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
