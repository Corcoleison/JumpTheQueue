using Devon4Net.Application.WebAPI.Configuration;
using Devon4Net.WebAPI.Implementation.Business.JumpTheQueueManagement.Hubs;
using Devon4Net.WebAPI.Implementation.Configure;
using Devon4Net.WebAPI.Implementation.Domain.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Application.WebAPI
{
    /// <summary>
    /// devonfw startup
    /// </summary>
    public class Startup
    {
        private IConfiguration Configuration { get; }

        /// <summary>
        /// Configuration variable with all settings file loaded
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
 
        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container. 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureDevonFw(Configuration);
            services.SetupDevonDependencyInjection(Configuration);
            services.AddControllers();
            services.AddOptions();
            services.AddMvc(option => option.EnableEndpointRouting = false)
                .AddJsonOptions(options => {options.JsonSerializerOptions.IgnoreNullValues = true;});
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                builder =>
                {
                    builder.WithOrigins("http://localhost:3000")
                    .AllowAnyHeader()
                    .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS", "HEAD")
                    .AllowCredentials();
                });
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">app net param</param>
        /// <param name="env">environment param</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHsts();
            app.UseStaticFiles();
            app.ConfigureDevonFw();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ColaHub>("/colahub");
            });
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}