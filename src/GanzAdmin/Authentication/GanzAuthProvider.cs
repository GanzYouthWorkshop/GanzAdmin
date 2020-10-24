using GanzAdmin.Database;
using GanzAdmin.Database.Models;
using GanzAdmin.Utils;
using Microsoft.AspNetCore.Http;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Permissions;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace GanzAdmin.Authentication
{
    public class GanzAuthProvider
    {
        //Folyamat:
        //-initialize-ban meg kell nézni a sütikat, ha van és érvényes -> bejelentkezés
        //-in-scope: ha van bejelentkezés, új süti létrehozása
        //-ha van kijelentkezés süzi törlése, bejelentkezés törlése

        public const string AUTH_COOKIE = "GanzAuth.Session";
        public const string JS_MAKE_COOKIE = "Blazor0.createCookie";
        public const string JS_REMOVE_COOKIE = "Blazor0.deleteCookie";

        private SessionManager m_SessionManager { get; set; }
        private IJSRuntime m_Javascript { get; set; }
        private IHttpContextAccessor m_Http;

        public SessionManager.Session CurrentSession { get; private set; }

        public Member CurrentMember
        {
            get
            {
                if(this.CurrentSession != null)
                {
                    return GanzAdminDbEngine.Instance.Members.FindById(this.CurrentSession.MemberId);
                }
                else
                {
                    return null;
                }
            }
        }

        public GanzAuthProvider(SessionManager sessionManager, IHttpContextAccessor httpProxy, IJSRuntime jsRuntime)
        {
            this.m_SessionManager = sessionManager;
            this.m_Http = httpProxy;
            this.m_Javascript = jsRuntime;

            if(this.m_Http.HttpContext != null)
            {
                IRequestCookieCollection cookies = this.m_Http.HttpContext.Request.Cookies;

                if (cookies.ContainsKey(AUTH_COOKIE))
                {
                    string cookie = cookies[AUTH_COOKIE];

                    this.CurrentSession = this.m_SessionManager.CheckTokenValidity(cookie);
                }
            }
        }

        public bool CheckAuth(string OrRoles, string AndRoles)
        {
            this.CurrentSession = this.m_SessionManager.CheckSessionValidity(this.CurrentSession);

            if (this.CurrentSession != null && this.CurrentSession.MemberId > 0)
            {
                GanzAdminDbEngine db = GanzAdminDbEngine.Instance;
                Member member = db.Members.FindById(this.CurrentSession.MemberId);
                if(string.IsNullOrEmpty(OrRoles) && string.IsNullOrEmpty(AndRoles))
                {
                    return true;
                }
                else if (OrRoles != null && (member.Roles.ContainsAny(OrRoles.Split(' ').ToList()) || member.Roles.Contains(Permissions.Overlord)))
                {
                    return true;
                }
                else if (AndRoles != null && (member.Roles.ContainsAll(AndRoles.Split(' ').ToList()) || member.Roles.Contains(Permissions.Overlord)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private async void MakeCookie(string name, string value, int daysToExpire)
        {
            await this.m_Javascript.InvokeVoidAsync(JS_MAKE_COOKIE, name, value, daysToExpire);
        }

        private void DeleteCookie(string name)
        {
            this.m_Javascript.InvokeVoidAsync(JS_REMOVE_COOKIE, name);
        }

        public bool TrySignIn(string user, string pass, bool remindMe)
        {
            bool result = false;

            GanzAdminDbEngine db = GanzAdminDbEngine.Instance;
            Member member = db.Members.FindOne(m => m.Username.ToLower() == user.ToLower());
            if(member != null && member.Password == GanzUtils.Sha256(pass))
            {
                this.SignIn(member, remindMe ? 30 : 1, remindMe);
                result = true;
            }

            return result;
        }

        public void SignIn(Member member, int daysUntilExpiration, bool remind)
        {
            SessionManager.Session session = this.m_SessionManager.RegisterNewSession(member.Id, daysUntilExpiration, remind);
            this.CurrentSession = session;
            this.MakeCookie(AUTH_COOKIE, session.SecurityToken, daysUntilExpiration);
        }

        public void SignOut()
        {
            this.m_SessionManager.RevokeSession(this.CurrentSession);
            this.DeleteCookie(AUTH_COOKIE);
        }

    }
}
