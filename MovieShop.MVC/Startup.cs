using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using Infrastructure.Data;
using Infrastructure.Filters;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MovieShop.MVC.Middlewares;
using Serilog;

namespace MovieShop.MVC
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
            services.AddControllersWithViews(
                options => options.Filters.Add(typeof(MovieShopHeaderFilter))
            );

            services.AddDbContext<MovieShopDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MovieShopDbConnection")));
            //services.AddScoped<IMovieService, MovieService>();   // It will reuse the same instance within HTTP request
            //services.AddScoped<IMovieService, MovieServiceTest>();
            //services.AddSingleton<IMovieService, MovieService>();    // rarely used. it will create instance and reuse same instance throughout the application until application dies.
            services.AddTransient<IMovieService, MovieService>();    // most corrupt. it will create instance each and every time. frequently used
            services.AddScoped<IMovieRepository, MovieRepository>();

            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IAsyncRepository<Genre>, EfRepository<Genre>>();

            services.AddScoped<ICastService, CastService>();
            services.AddScoped<IAsyncRepository<Cast>, CastRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddHttpContextAccessor();
            services.AddMemoryCache();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.Cookie.Name = "MovieShopAuthCookie";
                options.ExpireTimeSpan = TimeSpan.FromHours(2);
                options.LoginPath = "/Account/Login";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseExceptionHandler("/Home/Error");
                //app.UseDeveloperExceptionPage();
                app.UseMovieShopExceptionMiddleware();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // Serilog
            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
