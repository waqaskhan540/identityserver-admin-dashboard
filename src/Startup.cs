using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using IdentityServer.Extensions;
using IdentityServer.Constants;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer.Data;
using Microsoft.AspNetCore.Mvc.Razor;
using IdentityServer.Dashboard.Configuration;

namespace IdentityServer
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
           

            if (GlobalConfiguration.IsRunningTestMode)
            {
                services.AddTestIdentityServer()
                 .AddCorsPolicy();
            }else
            {
                var connectionString = Configuration.GetConnectionString("DefaultConnection");
                services.AddDatabaseConfiguration(connectionString)
                   .AddIdentityServerConfig(connectionString)
                   .AddServices()
                   .AddRepositories()
                   .AddProviders()
                   .AddCorsPolicy()
                   .AddAdminAccessPolicy();
            }
                  
            services.AddMvc();
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.AreaViewLocationFormats.Clear();
                options.AreaViewLocationFormats.Add("/Areas/{2}/Views/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Areas/{2}/Views/Shared/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
            });
        }

        public IConfiguration Configuration;
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                if (!GlobalConfiguration.IsRunningTestMode)
                {
                    using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                    {
                        serviceScope.ServiceProvider.GetService<DatabaseContext>().RunAllPendingMigrations();
                        serviceScope.ServiceProvider.GetService<ConfigurationDbContext>().RunAllPendingMigrations();
                    }
                }
            }

            if (!GlobalConfiguration.IsRunningTestMode)
            {
                app.EnsureDatabaseSeeded();
            }

            app.UseStaticFiles();
            app.UseCors(Policies.ID_SERVER_CORS_POLICY);
        
            app.UseIdentityServer();
            app.UseAuthentication();

         
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "areaRoute",
                  template: "{area:exists}/{controller=Account}/{action=Login}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "apiRoute",
                    template: "api/{controller}/{action}/{id?}");
              
            });
        }
    }
}
