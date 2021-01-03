using System;
using System.Collections.Generic;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public class Permissions
    {
        public const string _Placeholder = null;

        public const string ListMembers = nameof(ListMembers);
        public const string EditMembers = nameof(EditMembers);
        public const string DeleteMembers = nameof(DeleteMembers);

        public const string ListPayments = nameof(ListPayments);
        public const string EditPayments = nameof(EditPayments);
        public const string DeletePayments = nameof(DeletePayments);

        public const string ListLocations = nameof(ListLocations);
        public const string EditLocations = nameof(EditLocations);
        public const string DeleteLocations = nameof(DeleteLocations);

        public const string ListCategories = nameof(ListCategories);
        public const string EditCategories = nameof(EditCategories);
        public const string DeleteCategories = nameof(DeleteCategories);

        public const string ListParts = nameof(ListParts);
        public const string EditParts = nameof(EditParts);
        public const string DeleteParts = nameof(DeleteParts);
        public const string ManagePartParameters = nameof(ManagePartParameters);
        public const string ManagePartStock = nameof(ManagePartStock);

        public const string ListSuppliers = nameof(ListSuppliers);
        public const string EditSuppliers = nameof(EditSuppliers);
        public const string DeleteSuppliers = nameof(DeleteSuppliers);

        public const string AddProject = nameof(AddProject);
        public const string EditAnyProject = nameof(EditAnyProject);
        public const string DeleteProject = nameof(DeleteProject);

        public const string AddKit = nameof(AddKit);
        public const string EditKit = nameof(EditKit);
        public const string DeleteKit = nameof(DeleteKit);


        public const string AddCourse = nameof(AddCourse);
        public const string EditCourse = nameof(EditCourse);
        public const string DeleteCourse = nameof(DeleteCourse);

        public const string AccessOwnFileStorage = nameof(AccessOwnFileStorage);
        public const string AccessSharedFileStorage = nameof(AccessSharedFileStorage);
        public const string AccessSecretFileStorage = nameof(AccessSecretFileStorage);

        public const string AccessSeurityCameras = nameof(AccessSeurityCameras);
        public const string AccessNetworkDiagnostics = nameof(AccessNetworkDiagnostics);

        public const string Overlord = nameof(Overlord);

        #region Összes permission összegyűjtve
        public static List<string> AllPermissions { get; } = new List<string>()
        {
            ListMembers,
            EditMembers,
            DeleteMembers,

            ListPayments,
            EditPayments,
            DeletePayments,

            ListLocations,
            EditLocations,
            DeleteLocations,

            ListCategories,
            EditCategories,
            DeleteCategories,

            ListParts,
            EditParts,
            DeleteParts,
            ManagePartParameters,
            ManagePartStock,
            _Placeholder,

            ListSuppliers,
            EditSuppliers,
            DeleteSuppliers,

            AddProject,
            EditAnyProject,
            DeleteProject,

            AddKit,
            EditKit,
            DeleteKit,

            AddCourse,
            EditCourse,
            DeleteCourse,

            AccessOwnFileStorage,
            AccessSharedFileStorage,
            AccessSecretFileStorage,

            AccessSeurityCameras,
            AccessNetworkDiagnostics,
            _Placeholder,

            Overlord,
        };
        #endregion
    }
}
