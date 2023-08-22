using GanzAdmin.Database;
using GanzAdmin.Database.Models;
using GanzNet.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GanzAdmin.API.Auth
{
    public class AuthUnitProvider : IAuthUnitProvider
    {
        public string EnableAllPermission
        {
            get { return "Overlord"; }
        }

        public IUser Find(string username)
        {
            using(GanzAdminDbEngine db = GanzAdminDbEngine.GetInstance())
            {
                return db.GetCollection<Member>().Find(m => m.Username.ToLower() == username.ToLower()).FirstOrDefault();
            }
        }

        public List<string> GetPermissions(IUser user)
        {
            return (user as Member)?.Roles;
        }

        public IUser ProvideAuthUnit(long id)
        {
            using (GanzAdminDbEngine db = GanzAdminDbEngine.GetInstance())
            {
                return db.GetCollection<Member>().Find(m => m.Id == id).FirstOrDefault();
            }
        }
    }
}
