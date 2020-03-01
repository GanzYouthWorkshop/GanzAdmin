using System;
using System.Collections.Generic;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public class Roles
    {
        public const string AttendanceLogger = nameof(AttendanceLogger);

        public const string WarehouseBrowser = nameof(WarehouseBrowser);
        public const string WarehouseUploader = nameof(WarehouseUploader);
        public const string WarehouseManager = nameof(WarehouseManager);

        public const string MemberBrowser = nameof(WarehouseBrowser);
        public const string MemberPaymentSetter = nameof(WarehouseUploader);
        public const string MemberManager = nameof(WarehouseManager);

        public const string GeneralAministrator = nameof(GeneralAministrator);

        public const string SecurityCameraWatcher = nameof(SecurityCameraWatcher);

        public const string Overlord = nameof(Overlord);
    }
}
