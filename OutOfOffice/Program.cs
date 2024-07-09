using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OutOfOffice.Application.Services;
using OutOfOffice.Core.Models;
using OutOfOffice.Core.Stores;
using OutOfOffice.Persistence;
using OutOfOffice.Persistence.Repository;

namespace OutOfOffice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<OutOfOfficeDbContext>(options =>
                options.UseLazyLoadingProxies().UseSqlServer(connectionString, sqlOption => sqlOption.MigrationsAssembly("OutOfOffice.Persistence")));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<OutOfOfficeDbContext>()
                 .AddDefaultUI()
                 .AddDefaultTokenProviders();
           
            builder.Services.AddControllersWithViews();


            // Adding DIs
            builder.Services.AddScoped<IEmployeeStore, EmployeeRepository>();
            builder.Services.AddScoped<IProjectStore, ProjectRepository>();
            builder.Services.AddScoped<ILeaveRequestStore, LeaveRequestRepository>();
            builder.Services.AddScoped<IApprovalRequestStore, ApprovalRequestRepository>();

            builder.Services.AddScoped<EmployeeService>();
            builder.Services.AddScoped<ProjectService>();
            builder.Services.AddScoped<LeaveRequestService>();
            builder.Services.AddScoped<ApprovalRequestService>();


            var app = builder.Build();

            //Adding role and users to DB
            InitializeRolesAndAdminUser(app.Services).Wait();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }

        private static async Task InitializeRolesAndAdminUser(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                await DbSeeder.SeedDefaultData(scope.ServiceProvider);
            }
        }
    }
}
