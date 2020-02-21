using System;
using System.IO;
using System.Text;
using Gentings;
using Gentings.Data.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Yd
{
    /// <summary>
    /// 启动类。
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 初始化类<see cref="Startup"/>。
        /// </summary>
        /// <param name="configuration">配置实例。</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 配置接口实例。
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 配置IOC服务。
        /// </summary>
        /// <param name="services">服务实例对象。</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddGentings(Configuration)
                    .AddSqlServer();
            services.AddMvcCore()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                })
                .AddApiExplorer();
            AddJwtBearer(services);
            AddSwagger(services);
        }

        private string GetConfig(string key) => Configuration.GetSection(key)?.Value;

        private void AddJwtBearer(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = GetConfig("Jwt:Issuer"),
                        ValidAudience = GetConfig("Jwt:Audience"),
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GetConfig("Jwt:SecurityKey")))
                    };
                });
            services.AddAuthorization();
        }

        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                string contactName = GetConfig("SwaggerDoc:Contact:Name");
                string contactNameEmail = GetConfig("SwaggerDoc:Contact:Email");
                string contactUrl = GetConfig("SwaggerDoc:Contact:Url");
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = GetConfig("SwaggerDoc:Version"),
                    Title = GetConfig("SwaggerDoc:Title"),
                    Description = GetConfig("SwaggerDoc:Description"),
                    Contact = new OpenApiContact
                    { Name = contactName, Email = contactNameEmail, Url = new Uri(contactUrl) },
                    License = new OpenApiLicense { Name = contactName, Url = new Uri(contactUrl) }
                });

                IncludeXmlComments(options);
                //options.DocumentFilter<HiddenApiAttribute>(); // 在接口类、方法标记属性 [HiddenApi]，可以阻止【Swagger文档】生成
                options.OperationFilter<AddResponseHeadersFilter>();
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                options.OperationFilter<SecurityRequirementsOperationFilter>();
                //给api添加token令牌证书
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization", //jwt默认的参数名称
                    In = ParameterLocation.Header, //jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey
                });
            });
        }

        private void IncludeXmlComments(SwaggerGenOptions options)
        {
            var directory = new DirectoryInfo(Directory.GetCurrentDirectory());
            foreach (var file in directory.GetFiles("*.xml", SearchOption.TopDirectoryOnly))
            {
                options.IncludeXmlComments(file.FullName);
            }
        }

        /// <summary>
        /// 配置管道环境。
        /// </summary>
        /// <param name="app">应用程序构建实例。</param>
        /// <param name="env">环境变量。</param>
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            //下面两个位置一定要放对
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseGentings(Configuration);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapDefaultControllerRoute();
            });

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "API文档 V1");
                options.RoutePrefix = string.Empty;
            });
        }
    }
}
