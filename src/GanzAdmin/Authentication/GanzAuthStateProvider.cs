using GanzAdmin.Database;
using GanzAdmin.Database.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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

namespace GanzAdmin.Authentication
{
    //via https://docs.microsoft.com/en-us/aspnet/core/security/blazor/?view=aspnetcore-3.1&tabs=visual-studio
    public class GanzAuthStateProvider : AuthenticationStateProvider
    {
        //TODO: Ennek itt rohadtul nem staticnak kéne lennie!
        private IHttpClientFactory m_Http;

        public GanzAuthStateProvider(IHttpClientFactory httpContextAccessor)
        {
            m_Http = httpContextAccessor;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var c = this.m_Http.CreateClient();
           //var test = this.m_Http..User.Claims;
           //return Task<AuthenticationState>.Run((() =>
           //{
           //    //this.m_HttpContextAccessor.HttpContext.Request.Cookies;

           //    using (GanzAdminDbContext db = new GanzAdminDbContext())
           //    {
           //        Member user = db.Members.FirstOrDefault();

           //        ClaimsIdentity identity = null;

           //        if (user != null)
           //        {
           //            identity = new ClaimsIdentity(new[]
           //            {
           //                new Claim(ClaimTypes.Name, "mrfibuli"),
           //            }, CookieAuthenticationDefaults.AuthenticationScheme);

           //        }

           //        var authProperties = new AuthenticationProperties
           //        {
           //            IsPersistent = true,
           //        };

           //        var principal = new ClaimsPrincipal(identity);
           //        new AuthenticationState(principal);
           //    }
           //});

            //Ha identity == null akkor nem vagyunk bejelentkezve, különben a megadott hozzáférésekkel (claimekkel) vagyunk bejelentkezve
            ClaimsPrincipal user = new ClaimsPrincipal();

            return Task.FromResult(new AuthenticationState(user));
        }

        public static async Task SignIn(HttpContext context, Member member)
        {
            ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            identity.AddClaim(new Claim(ClaimTypes.Name, member.Username));
            identity.AddClaim(new Claim(ClaimTypes.Role, "Administrator"));

            ClaimsPrincipal user = new ClaimsPrincipal(identity);

            AuthenticationProperties authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                IsPersistent = true,
                IssuedUtc = DateTimeOffset.UtcNow,
            };

            var x = await context.AuthenticateAsync();
            await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user, authProperties);
        }

        public static async Task SignOut(HttpContext context)
        {
            await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
