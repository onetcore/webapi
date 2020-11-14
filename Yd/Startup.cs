using System.Text.Json;
using Gentings;
using Gentings.AspNetCore;
using Gentings.AspNetCore.WebSockets;
using Gentings.Data.SqlServer;
using Gentings.Extensions.EventLogging;
using Gentings.Extensions.Notifications;
using Gentings.Extensions.Settings;
using Gentings.Storages;
using Gentings.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Yd
{
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
            services.AddGentings(Configuration)
                .AddSqlServer()
                .AddNotification()
                .AddEventLoggers()
                .AddTaskServices()
                .AddMediaStorages()
                .AddSettingDictionary();
            services.AddMvcCore()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                })
                .AddApiExplorer();
            services.AddRazorPages().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
            app.UseWebSockets().UseWebSocketHandler();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            //下面两个位置一定要放对
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseGentings(Configuration);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
