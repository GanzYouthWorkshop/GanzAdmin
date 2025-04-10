using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GanzAdmin.Database;
using Microsoft.AspNetCore.Components.Authorization;
using GanzAdmin.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using GanzAdmin.Configuration;
using GanzAdmin.API.NVR;
using GanzAdmin.Tools;
using GanzAdmin.Scheduling;
using GanzNet.Authentication;
using GanzAdmin.API.Auth;
using GanzAdmin.UserMessages;

namespace GanzAdmin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => options.EnableEndpointRouting = false);//.SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddOptions();
            
            services.AddHttpContextAccessor();
            services.AddSingleton<SessionManager>();
            services.AddSingleton<UserMessageService>();
            services.AddScoped<IAuthUnitProvider, AuthUnitProvider>();
            services.AddScoped<IAuthProvider, AuthProvider>();
            services.AddScoped<ToolService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            GanzAdminConfiguration.Instance = GanzAdminConfiguration.Load("config.xml");


            GanzAdminDbEngine.Instance = new GanzAdminDbEngine(GanzAdminConfiguration.Instance.DatabaseConnectionString);
            using (GanzAdminDbEngine db = GanzAdminDbEngine.GetInstance())
            {
                db.EnsureCreated();
            }

            CameraHandler.Start();

            Scheduler.Tasks.Add(new StatisticsScheduledTask());
            //Scheduler.Tasks.Add(new CameraRestartScheduledTask());
            Scheduler.Start();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseMvcWithDefaultRoute();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
