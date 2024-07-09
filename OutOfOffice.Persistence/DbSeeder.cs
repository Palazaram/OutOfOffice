using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using OutOfOffice.Core.Models;
using OutOfOffice.Persistence.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Persistence
{
    public class DbSeeder
    {
        public static async Task SeedDefaultData(IServiceProvider service)
        {
            var userMgr = service.GetService<UserManager<ApplicationUser>>();
            var roleMgr = service.GetService<RoleManager<IdentityRole>>();

            //adding roles to db
            await roleMgr.CreateAsync(new IdentityRole(Roles.Employee.ToString()));
            await roleMgr.CreateAsync(new IdentityRole(Roles.HRManager.ToString()));
            await roleMgr.CreateAsync(new IdentityRole(Roles.ProjectManager.ToString()));
            await roleMgr.CreateAsync(new IdentityRole(Roles.Administrator.ToString()));


            //create admin user
            var admin = new ApplicationUser
            {
                Login = "admin",
                UserName = "Administrator1",
                Email = "admin@gmail.com"
            };

            var userInDb = await userMgr.FindByEmailAsync(admin.Email);

            if (userInDb is null)
            {
                await userMgr.CreateAsync(admin, "Admin@123");
                await userMgr.AddToRoleAsync(admin, Roles.Administrator.ToString());
            }

            //create projectManager user
            var projectManager = new ApplicationUser
            {
                Login = "projectManager",
                UserName = "ProjectManager1",
                Email = "projMgr@gmail.com"
            };

            userInDb = await userMgr.FindByEmailAsync(projectManager.Email);

            if (userInDb is null)
            {
                await userMgr.CreateAsync(projectManager, "ProjMgr@123");
                await userMgr.AddToRoleAsync(projectManager, Roles.ProjectManager.ToString());
            }

            //create hrManager user
            var hrManager = new ApplicationUser
            {
                Login = "hrManger",
                UserName = "HR_Manger1",
                Email = "hrMgr@gmail.com"
            };

            userInDb = await userMgr.FindByEmailAsync(hrManager.Email);

            if (userInDb is null)
            {
                await userMgr.CreateAsync(hrManager, "HRMgr@123");
                await userMgr.AddToRoleAsync(hrManager, Roles.HRManager.ToString());
            }

            //create employee user
            var employee = new ApplicationUser
            {
                Login = "employee",
                UserName = "Employee1",
                Email = "emp@gmail.com"
            };

            userInDb = await userMgr.FindByEmailAsync(employee.Email);

            if (userInDb is null)
            {
                await userMgr.CreateAsync(employee, "Emp@123");
                await userMgr.AddToRoleAsync(employee, Roles.Employee.ToString());
            }
        }
    }
}
