using System;
using System.Collections.Generic;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public class Permissions
    {
        public const string ListMembers = nameof(ListMembers);
        public const string EditMembers = nameof(EditMembers);
        public const string DeleteMembers = nameof(DeleteMembers);

        public const string ListLocations = nameof(ListLocations);
        public const string EditLocations = nameof(EditLocations);
        public const string DeleteLocations = nameof(DeleteLocations);

        public const string AccessOwnFileStorage = nameof(AccessOwnFileStorage);
        public const string AccessSharedFileStorage = nameof(AccessSharedFileStorage);

        public const string Overlord = nameof(Overlord);

        #region Összes permission összegyűjtve
        public static List<string> AllPermissions { get; } = new List<string>()
        {
            ListMembers,
            EditMembers,
            DeleteMembers,

            ListLocations,
            EditLocations,
            DeleteLocations,

            AccessOwnFileStorage,
            AccessSharedFileStorage,

            Overlord,
        };
        #endregion
    }
}
