using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanzNet.Authentication
{
    public interface IAuthProvider
    {
        bool CheckAuth(string OrRoles, string AndRoles);
    }
}
