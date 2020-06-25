using System;
using System.Collections.Generic;
using System.Text;

namespace GanzAdmin.Utils
{
    public class SearchFragment
    {
        public enum ExpressionType
        {
            Main,
            GreaterThan,
            LessThan,
            EqualTo,
            SetTo,
            Attribute
        }

        public string Key { get; set; }
        public ExpressionType Type { get; set; }
        public string Value { get; set; }
    }
}
