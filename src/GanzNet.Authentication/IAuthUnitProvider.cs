using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GanzNet.Authentication
{
    public interface IAuthUnitProvider
    {
        string EnableAllPermission { get; }

        IUser ProvideAuthUnit(long id);
        IUser Find(string username);
        List<string> GetPermissions(IUser user);
    }
}
