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
        public IJSRuntime m_Javascript { get; set; }
        private IHttpContextAccessor m_Http;

        private SessionManager.Session m_ScopeSession;

        public GanzAuthProvider(SessionManager sessionManager, IHttpContextAccessor httpProxy, IJSRuntime jsRuntime)
        {
            this.m_SessionManager = sessionManager;
            this.m_Http = httpProxy;
            this.m_Javascript = jsRuntime;

            IRequestCookieCollection cookies = this.m_Http.HttpContext.Request.Cookies;

            if (cookies.ContainsKey(AUTH_COOKIE))
            {
                var cookie = cookies[AUTH_COOKIE];

                this.m_ScopeSession = this.m_SessionManager.CheckTokenValidity(cookie);
            }
        }

        public bool CheckAuth(string OrRoles, string AndRoles)
        {
            this.m_ScopeSession = this.m_SessionManager.CheckSessionValidity(this.m_ScopeSession);

            if (this.m_ScopeSession != null && this.m_ScopeSession.MemberId > 0)
            {
                if(OrRoles != null)
                {
                    //TODO
                    return true;
                }
                else if(AndRoles != null)
                {
                    //TODO
                    return true;
                }
                else
                {
                    return true;
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

        public bool TrySignIn(string user, string pass)
        {
            bool result = false;

            using(GanzAdminDbContext db = new GanzAdminDbContext())
            {
                Member member = db.Members.FindOne(m => m.Username == user);
                if(member != null && member.Password == GanzUtils.Sha256(pass))
                {
                    this.SignIn(member);
                    result = true;
                }
            }

            return result;
        }

        public async Task SignIn(Member member)
        {
            SessionManager.Session session = this.m_SessionManager.RegisterNewSession(member.Id);
            this.m_ScopeSession = session;
            this.MakeCookie(AUTH_COOKIE, session.SecurityToken, 3);
        }

        public async Task SignOut()
        {
            this.m_SessionManager.RevokeSession(this.m_ScopeSession);
            this.DeleteCookie(AUTH_COOKIE);
        }

    }
}
