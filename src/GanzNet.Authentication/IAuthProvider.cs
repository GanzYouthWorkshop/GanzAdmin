using GanzAdmin.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanzNet.Authentication
{
    public interface IAuthProvider
    {
        //TODO this does not belong here!
        IUser CurrentUser { get; }
        bool CheckAuth(string OrRoles, string AndRoles);

        bool TrySignIn(string user, string pass, bool remindMe);
        void SignOut();
    }
}
