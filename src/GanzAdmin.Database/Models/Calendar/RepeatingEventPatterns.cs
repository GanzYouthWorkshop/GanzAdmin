using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanzAdmin.Database.Models.Calendar
{
    [Flags]
    public enum RepeatingEventPatterns
    {
        None = 0,
        Daily = 1,
        Weekly = 2,
        Monthly = 4
    }
}
