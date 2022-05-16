using JustSpeak.Common;
using JustSpeak.Data;
using JustSpeak.Interfaces;
using JustSpeak.Servicehelper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace JustSpeak
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
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            services.AddSingleton(Configuration);
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<IConstants, Constants>();
            services.AddTransient<IServiceHelper, ServiceHelper>();
            services.AddControllersWithViews();
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                      policy =>
                                      {
                                          policy.AllowAnyHeader()
                                                .AllowAnyMethod();
                                      });
            });
            services.AddDbContext<JustSpeakContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("JustSpeakContext")));
            
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
                app.UseExceptionHandler("/StandardSpeech/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=StandardSpeech}/{action=Index}/{id?}");
            });
        }
    }
}
