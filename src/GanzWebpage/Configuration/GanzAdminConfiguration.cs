using GEV.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GanzWebpage.Configuration
{
    //TODO: Save and load!
    [Serializable]
    public class GanzWebConfiguration : Configuration<GanzWebConfiguration>
    {
        public static GanzWebConfiguration Instance { get; set; }

        public string DatabaseConnectionString { get; set; } = @"Filename=..\GanzAdmin\_Data\ganzadmin.db;connection=shared";
    }
}
