using GanzAdmin.Utils;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public class ManufacturingMachine : IEntity
    {
        public enum DeviceTypes
        {
            OctoPrint,
            Cura
        }

        public enum States
        {
            Unknown,
            Idle,
            Working,
            Error
        }

        [BsonId]
        public long Id { get; set; }

        public string Name { get; set; }
        public DeviceTypes DeviceType { get; set; }
        public ManufacturingItem CurrentWorkitem { get; set; }
        public List<ManufacturingItem> WorkitemsInQueue { get; set; }

        //[BsonIgnore]
        //public IManufacturingDeviceHandler DeviceHandler { get; set; }

        [BsonIgnore]
        public string DisplayValue
        {
            get { return this.Name; }
        }

        public static List<MemberProject> Search(IEnumerable<MemberProject> projects, List<SearchFragment> expression)
        {
            List<MemberProject> list = new List<MemberProject>();
            list.AddRange(projects);
            IEnumerable<MemberProject> result = list;

            foreach (SearchFragment fragment in expression)
            {
                string searchKey = fragment.Key.Trim().ToLower();
                string searchVal = fragment.Value?.Trim().ToLower();
                float searchNumeric = float.NaN;
                float.TryParse(searchVal, out searchNumeric);

                if (fragment.Type == SearchFragment.ExpressionType.Main)
                {
                    if (searchKey != "")
                    {
                        result = result.Where(t =>
                            (t.Name != null && t.Name.ToLower().Contains(searchKey)) ||
                            (t.Description != null && t.Description.ToLower().Contains(searchKey))
                            );
                    }
                }
            }
            return result.ToList();
        }
    }
}
