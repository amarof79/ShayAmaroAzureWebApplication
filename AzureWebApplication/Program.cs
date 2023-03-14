using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AzureWebApplication.Data;
using Microsoft.AspNetCore.Identity;
using System.Configuration;
using System;

namespace AzureWebApplication {
    public class Program {
        public static void Main(string[] args) {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddDbContext<AzureWebApplicationContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("AzureWebApplicationContext") ?? throw new InvalidOperationException("Connection string 'AzureWebApplicationContext' not found.")));

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AzureWebApplicationContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment()) {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            /* public void ConfigureServices(IServiceCollection services) {
                services.AddDbContext<AzureWebApplicationContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

                services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<AzureWebApplicationContext>();

                services.AddControllersWithViews();
                services.AddRazorPages();
            } */

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();

        }

        public void ConfigureServices(IServiceCollection services, string[] args) {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            services.AddDbContext<AzureWebApplicationContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("AzureWebApplicationContext")));

            services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<AzureWebApplicationContext>();

            services.AddControllersWithViews();
            services.AddRazorPages();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

        }

    }
}
