//@QnSBaseCode
//MdStart
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace QuickNSmart.AspMvc
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            BeginConfigureServices(services);
            var mvcviews = services.AddControllersWithViews();

#if (DEBUG)
            mvcviews.AddRazorRuntimeCompilation();
#endif

            services.AddSession(options =>
            {
                options.Cookie.IsEssential = true;
                options.Cookie.Name = $".{nameof(QuickNSmart)}.Session";
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromMinutes(20);
            });
            services.AddSingleton<IFactoryWrapper, FactoryWrapper>();
            EndConfigureServices(services);
        }
        partial void BeginConfigureServices(IServiceCollection services);
        partial void EndConfigureServices(IServiceCollection services);

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            BeginConfigure(app, env);

            // Transfer the application settings to the logic.
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Logic.Modules.Configuration.Settings.SetConfiguration(builder.Build());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            EndConfigure(app, env);
        }
        partial void BeginConfigure(IApplicationBuilder app, IWebHostEnvironment env);
        partial void EndConfigure(IApplicationBuilder app, IWebHostEnvironment env);
    }
}
//MdEnd