using GanzAdmin.Database;
using GanzAdmin.Database.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Permissions;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.JSInterop;

namespace GanzAdmin.Authentication
{
    public class GanzAuthProvider
    {

        public const string AUTH_COOKIE = "GanzAuth.Session";
        public const string JS_MAKE_COOKIE = "Blazor.createCookie";
        public const string JS_REMOVE_COOKIE = "Blazor.deleteCookie";


        private SessionManager m_Session { get; set; }
        public IJSRuntime m_Javascript { get; set; }
        private IHttpContextAccessor m_Http;

        //Folyamat:
        //-initialize-ban meg kell nézni a sütikat, ha van és érvényes -> bejelentkezés
        //-in-scope: ha van bejelentkezés, új süti létrehozása
        //-ha van kijelentkezés süzi törlése, bejelentkezés törlése

        public GanzAuthProvider(SessionManager sessionManager, IHttpContextAccessor httpProxy, IJSRuntime jsRuntime)
        {
            this.m_Session = sessionManager;
            this.m_Http = httpProxy;
            this.m_Javascript = jsRuntime;

            IRequestCookieCollection cookies = this.m_Http.HttpContext.Request.Cookies;

            if (cookies.ContainsKey(AUTH_COOKIE))
            {
                var cookie = cookies[AUTH_COOKIE];

                int member = this.m_Session.CheckTokenValidity(cookie);
                if(member > 0)
                {
                    //TODO: itt tárolni kéne, hogy amúgy ki van belépve
                }
            }
        }

        public bool CheckAuth(string OrRoles, string AndRoles)
        {
            //TODO: meg kell írni rendesen
            return false;
        }

        private void MakeCookie(string name, string value, int daysToExpire)
        {
            this.m_Javascript.InvokeVoidAsync(JS_MAKE_COOKIE, name, value, daysToExpire);
        }

        private void DeleteCookie(string name)
        {
            this.m_Javascript.InvokeVoidAsync(JS_REMOVE_COOKIE, name);
        }

        public async Task SignIn(Member member)
        {
            SessionManager.Session session = this.m_Session.RegisterNewSession(member.MemberId);
            this.MakeCookie(AUTH_COOKIE, session.SecurityToken, 3);
        }

        public async Task SignOut()
        {
            //Itt kéne tudni ki hogyan van belépve mert revoke-olni kéne a sessiont
            this.DeleteCookie(AUTH_COOKIE);
        }

    }
}
