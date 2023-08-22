using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanzNet.Authentication
{
    public interface IAuthProvider
    {
        SessionManager.Session CurrentSession { get; }
        IUser CurrentMember { get; }

        bool TrySignIn(string user, string pass, bool remindMe);
        void SignOut();

        bool CheckAuth(string OrRoles, string AndRoles);
    }
}
