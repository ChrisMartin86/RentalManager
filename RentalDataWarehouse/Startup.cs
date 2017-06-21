using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RentalDataWarehouse.Data;
using RentalDataWarehouse.Models;
using RentalDataWarehouse.Services;

namespace RentalDataWarehouse
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            populateConfigurationData();
        }

        private void populateConfigurationData()
        {
            Dictionary<string, string> connectionStrings = createConnectionStringDictionary();
            Dictionary<string, string> appSettings = createAppSettingsDictionary();
            Dictionary<string, string> businessInfo = createBusinessInfoDictionary();

            ConfigurationData.Populate(connectionStrings, appSettings, businessInfo);
        }

        private Dictionary<string, string> createConnectionStringDictionary()
        {
            var connectionStrings = new Dictionary<string, string>();
                connectionStrings.Add("DefaultConnection", Configuration.GetConnectionString("DefaultConnection"));

            return connectionStrings;
        }

        private Dictionary<string, string> createAppSettingsDictionary()
        {
            var appSettings = new Dictionary<string, string>();
                appSettings.Add("TMDbv3Key", Configuration.GetSection("AppSettings")["IMDbv3Key"]);
                appSettings.Add("TMDbv4Key", Configuration.GetSection("AppSettings")["TMDbv4Key"]);

            return appSettings;
        }

        private Dictionary<string, string> createBusinessInfoDictionary()
        {
            var businessInfo = new Dictionary<string, string>();
                businessInfo.Add("Name", Configuration.GetSection("BusinessInfo")["Name"]);
                businessInfo.Add("About", Configuration.GetSection("BusinessInfo")["About"]);
                businessInfo.Add("Address", Configuration.GetSection("BusinessInfo")["Address"]);
                businessInfo.Add("Phone", Configuration.GetSection("BusinessInfo")["Phone"]);
                businessInfo.Add("SupportEmail", Configuration.GetSection("BusinessInfo")["SupportEmail"]);
                businessInfo.Add("PrimaryEmail", Configuration.GetSection("BusinessInfo")["PrimaryEmail"]);

            return businessInfo;
        }
    }
}
