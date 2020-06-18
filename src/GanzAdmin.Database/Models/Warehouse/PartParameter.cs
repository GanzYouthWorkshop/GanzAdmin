using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public class PartParameter
    {
        public string Name { get; set; }
        public string DefaultValue { get; set; }
        public string Value { get; set; }
        public string Unit { get; set; }

        public bool IsSelectValue
        {
            get
            {
                if(this.DefaultValue != null)
                {
                    return this.DefaultValue.Contains("|");
                }
                return false;
            }
        }

        public List<string> SelectOptions
        {
            get
            {
                if (this.DefaultValue != null)
                {
                    return this.DefaultValue.Split("|").ToList();
                }
                return null;
            }
        }
    }
}
