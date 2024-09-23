using LiteDB;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public class GlobalSetting : IEntity
    {
        public enum PresetSettings
        {
            DefaultWikiPage
        }

        [BsonId]
        public long Id { get; set; }

        public string Name { get; set; }
        public string Value { get; set; }

        public string DisplayValue => Name;

        public static string GetSetting(string name)
        {
            string result = null;

            using (GanzAdminDbEngine db = GanzAdminDbEngine.GetInstance())
            {
                GlobalSetting setting = db.GetCollection<GlobalSetting>().FindOne(set => set.Name == name);
                if (setting != null)
                {
                    result = setting.Value;
                }
                else
                {
                    if (name == nameof(PresetSettings.DefaultWikiPage))
                    {
                        result = "1";
                    }
                }
            }

            return result;
        }

        public static void SaveSetting(string name, string value)
        {
            string result = null;

            using (GanzAdminDbEngine db = GanzAdminDbEngine.GetInstance())
            {
                GlobalSetting setting = db.GlobalSettings.FindOne(set => set.Name == name);
                if (setting != null)
                {
                    setting.Value = value;

                    db.BeginTransaction();
                    db.GlobalSettings.Update(setting);
                    db.Transact();
                }
                else
                {
                    db.BeginTransaction();
                    db.GlobalSettings.Insert(new GlobalSetting()
                    {
                        Name = name,
                        Value = value
                    });
                    db.Transact();
                }
            }
        }
    }
}
