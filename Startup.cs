using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using DiscussionMVCAppOaks.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DiscussionMVCAppOaks.Models;
using DiscussionMVCAppOaks.Models.LotModel;
using DiscussionMVCAppOaks.Models.LotTypeModel;
using DiscussionMVCAppOaks.Models.PermitModel;
using DiscussionMVCAppOaks.Models.LotStatusModel;
using Microsoft.AspNetCore.Identity.UI.Services;
using DiscussionMVCAppOaks.Services;

namespace DiscussionMVCAppOaks
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>
               (
                   options =>
                   {
                       options.Password.RequiredLength = 6;
                       options.Password.RequireNonAlphanumeric = false;
                       options.Password.RequireUppercase = true;
                       options.Password.RequireLowercase = true;
                       options.Password.RequireDigit = true;
                       options.User.RequireUniqueEmail = true;
                       options.SignIn.RequireConfirmedEmail = true;
                   }
               )
           .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddTransient<ILotRepo, LotRepo>();
            services.AddTransient<ILotTypeRepo, LotTypeRepo>();
            services.AddTransient<IApplicationUserRepo, ApplicationUserRepo>();
            services.AddTransient<IPermitRepo, PermitRepo>();
            services.AddTransient<ILotStatusRepo, LotStatusRepo>();
            services.AddTransient<IDepartmentRepo, DepartmentRepo>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddSession();

            services.AddCloudscribePagination();

            services.AddControllersWithViews();
            services.AddRazorPages();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
