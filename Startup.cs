using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AspNetCoreTodo.Data;
using AspNetCoreTodo.Models;
using AspNetCoreTodo.Services;

namespace AspNetCoreTodo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void Configure(IApplicationBuilder app,
    IHostingEnvironment env,
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager)
{
    private static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
{
    var alreadyExists = await roleManager.RoleExistsAsync(Constants.AdministratorRole);

    if (alreadyExists) return;

    await roleManager.CreateAsync(new IdentityRole(Constants.AdministratorRole));
}
}        public void ConfigureServices(IServiceCollection services)
        {
            if (env.IsDevelopment())
{
    // (... some code)

    // Make sure there's a test admin account
    EnsureRolesAsync(roleManager).Wait();
    EnsureTestAdminAsync(userManager).Wait();
}
            services
    .AddAuthentication()
    .AddFacebook(options =>
    {
        options.AppId = Configuration["Facebook:AppId"];
        options.AppSecret = Configuration["Facebook:AppSecret"];
    });
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
         services.AddScoped<ITodoItemService, TodoItemService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
