using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Data.Entity;
using Microsoft.Extensions.PlatformAbstractions;
using Contacts.Infrastructure;
using Microsoft.AspNet.Mvc.Formatters;
using Newtonsoft.Json.Serialization;

namespace Contacts
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc(options =>
            {
                var json = options.OutputFormatters
                    .OfType<JsonOutputFormatter>()
                    .FirstOrDefault();
                if (json != null)
                {
                    json.SerializerSettings.ContractResolver =
                        new CamelCasePropertyNamesContractResolver();
                }
            });
            
            var path = PlatformServices.Default.Application.ApplicationBasePath;
            
            services.AddEntityFramework()
                .AddSqlite()
                .AddDbContext<ContactsContext>(options => options.UseSqlite("Filename=" + Path.Combine(path, "contacts.db")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();
            loggerFactory.AddConsole(LogLevel.Information);
            
            app.UseIISPlatformHandler();

            app.UseStaticFiles();

            app.UseMvc();
            
        }

        // Entry point for the application.
        public static void Main(string[] args) => Microsoft.AspNet.Hosting.WebApplication.Run<Startup>(args);
    }
}
