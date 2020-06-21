using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using VseTut.Core.Users.Model;
using VseTut.EntityFrameworkCore.EntityFrameworkCore;
using VseTut.EntityFrameworkCore.EntityFrameworkCore.Repositories;
using VseTut.Web.Host;
using AutoMapper;
using VseTut.Web.Core;
using VseTut.Core.Auth;
using VseTut.Web.Core.Managers.Auth;
using VseTut.Core.Categories;
using VseTut.Web.Core.Managers.Categories;
using VseTut.Core.SubCategories;
using VseTut.Web.Core.Managers.SubCategories;

namespace VseTut
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
            services.AddDbContext<VseTutDbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>()
               .AddEntityFrameworkStores<VseTutDbContext>()
               .AddDefaultTokenProviders();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthManager, AuthManager>();
            services.AddScoped<IJwtManager, JwtManager>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<ICategoryManager, CategoryManager>();
            services.AddScoped<ISubCategoryManager, SubCategoriesManager>();

            services.AddScoped<ClaimsIdentityFactory>();
            services.AddScoped<VseTutDbContext>();
            services.AddScoped<IdentityOptions>();
            services.AddScoped<User>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvc(options =>
            {
                options.OutputFormatters.Insert(0, new CustomFormatter());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "api/{controller=Auth}/{actions=Authenticate}/{id?}");
            });
        }
    }
}
