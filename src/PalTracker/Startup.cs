using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace PalTracker
{
    public static class CloudFoundryInfoExtensions
    {
        public static IServiceCollection CloudFoundryInfoConfigureServices(this IServiceCollection services, IConfiguration Configuration)
        {
            string port = Configuration.GetValue<string>("PORT", "PORT not configured.");
            string memoryLimit = Configuration.GetValue<string>("MEMORY_LIMIT", "MEMORY_LIMIT not configured.");
            string cfInstanceIndex = Configuration.GetValue<string>("CF_INSTANCE_INDEX", "CF_INSTANCE_INDEX not configured.");
            string cfInstanceAddr = Configuration.GetValue<string>("CF_INSTANCE_ADDR", "CF_INSTANCE_ADDR not configured.");
        
        
            CloudFoundryInfo info = new CloudFoundryInfo(port, memoryLimit,cfInstanceIndex, cfInstanceAddr);
            services.AddSingleton(info);

            return services;
        }
    }
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            string message = Configuration.GetValue<string>("WELCOME_MESSAGE", "WELCOME_MESSAGE not configured.");

            services
            .AddSingleton(sp => new WelcomeMessage(message))
            .CloudFoundryInfoConfigureServices(Configuration);

             
        }
       
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
