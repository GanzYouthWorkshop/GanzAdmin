﻿using GEV.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GanzAdmin.Configuration
{
    //TODO: Save and load!
    [Serializable]
    public class GanzAdminConfiguration : Configuration<GanzAdminConfiguration>
    {
        public static GanzAdminConfiguration Instance { get; set; }

        public string DatabaseConnectionString { get; set; } = @"Filename=_Data\ganzadmin.db; Mode=Shared";
        public string MyIP { get; set; } = "87.97.60.25";

        public string SharedFileFolder { get; set; } = "";
        public string SecretFileFolder { get; set; } = "";
        public string OwnFleFolder { get; set; } = "";

        public string NvrFolder { get; set; } = @"X:\";
        public string OpenClosedFolder { get; set; } = @"C:\WEB\OpenClosed\";
    }
}
