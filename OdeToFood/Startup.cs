using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OdeToFood.Data;

namespace OdeToFood
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
            services.AddScoped<IRestaurantData, SqlRestaurantData>();
            services.AddDbContextPool<OdeToFoodDbContext>(Options=> {
                Options.UseSqlServer(Configuration.GetConnectionString("OdeToFoodDb"));
            });
            services.AddRazorPages();
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseNodeModules();
            app.UseRouting();
            app.Use(SayHelloMiddlewear);
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }

        private RequestDelegate SayHelloMiddlewear(RequestDelegate arg)
        {
            return async ctx =>
            {
                if (ctx.Request.Path.StartsWithSegments("/hello"))
                {
                    await ctx.Response.WriteAsync("Hello , World!");
                }
                else
                {
                    await arg(ctx);
                }
            };
        }
    }
}
