using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public class GlobalSetting
    {
        public enum PresetSettings
        {
            DefaultWikiPage
        }

        [BsonId]
        public long Id { get; set; }

        public string Name { get; set; }
        public string Value { get; set; }

        public static string GetSetting(string name)
        {
            string result = null;

            GlobalSetting set = GanzAdminDbEngine.Instance.GetCollection<GlobalSetting>().FindOne(set => set.Name == name);
            if(set != null)
            {
                result = set.Value;
            }
            else
            {
                if(name == nameof(PresetSettings.DefaultWikiPage))
                {
                    result = "1";
                }
            }

            return result;
        }
    }
}
