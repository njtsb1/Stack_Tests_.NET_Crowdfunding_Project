using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NToastNotify;
using Crowdfunding.MVC.Extensions;

namespace Crowdfunding.MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddNToastNotifyNoty(new NotyOptions
            {
                ProgressBar = true,
                Timeout = 5000                
            }, new NToastNotifyOption
            {
                DefaultSuccessTitle = "Yeah!",
                DefaultSuccessMessage = "Successfully performed operation!",

                DefaultErrorTitle = "Ops!",
                DefaultErrorMessage = "Something went wrong!"               

            }).AddRazorRuntimeCompilation();

            services
                .AddDbContext()
                .AddIocConfiguration(Configuration)
                .AddAutoMapper(Configuration)
                .AddCustomConfiguration(Configuration);

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
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

            app.UseNToastNotify();
        }
    }
}
