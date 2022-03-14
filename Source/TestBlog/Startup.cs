using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestBlog.Interfaces;
using TestBlog.Services;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace TestBlog
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
            services.AddHttpContextAccessor();
            // Session injection for use of http session
            services.AddTransient<ISesion, Sesion>();
            services.AddControllersWithViews();
            services.AddDistributedMemoryCache();
            services.AddSession();
            // Blog services injection
            services.AddTransient<IUser, UserService>();
            services.AddTransient<IComment, CommentService>();
            services.AddTransient<IPost, PostService>();
            
            //Add cookies authentication
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
                x => {
                    x.AccessDeniedPath = "/Error/Index";
                    x.Events = new Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationEvents()
                    {
                        OnRedirectToAccessDenied = ctx =>
                        {
                            ctx.Response.Redirect("/Error/Index?errormsg=User is not authorized to use this option");
                            return Task.CompletedTask;
                        }
                    };
                    x.LoginPath = "/Login/Index";
                    });
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=PostList}/{action=Index}/{id?}");
            });

        }
    }
}
