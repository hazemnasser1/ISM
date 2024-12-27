using Company.BLL.Interfaces;
using Company.BLL.Reposatories;
using Company.DAL.Contexts;
using Company.DAL.Models;
using Company.PL.Helper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Company
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped(typeof(IGenaricReposatory<>), typeof(GenericRepository<>));
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddDbContext<CompanyDBContext>(options =>
                options.UseSqlServer("Server=localhost\\MSSQLSERVER01;database=companyDB;trusted_connection=true;encrypt=false")
            );
			builder.Services.AddIdentity<User, IdentityRole>(config =>
			{
				config.Lockout.MaxFailedAccessAttempts = 3;
				config.Password.RequiredUniqueChars = 1;
				config.Password.RequireUppercase = true;
			}).AddEntityFrameworkStores<CompanyDBContext>().AddDefaultTokenProviders();


			builder.Services.AddAutoMapper(typeof(MappSetting));

            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
			{
				options.LoginPath = "/Account/Login";
				options.AccessDeniedPath = "/Home/Error";
				options.ExpireTimeSpan = TimeSpan.FromHours(12);
			});

            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            var scope = app.Services.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var roles = new[] { "Leader", "Member" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Home}/{id?}");

            app.Run();
        }
    }
}
