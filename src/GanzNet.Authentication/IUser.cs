using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanzNet.Authentication
{
    public interface IUser
    {
        long Uid { get; }

        string Username { get; }
        string Password { get; }
    }
}
