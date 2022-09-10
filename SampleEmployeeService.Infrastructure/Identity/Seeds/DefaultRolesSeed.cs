using SampleEmployeeService.Domain.Helpers;
using SampleEmployeeService.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleEmployeeService.Domain.Enums;

namespace SampleEmployeeService.Infrastructure.Identity.Seeds
{
    public static class DefaultRolesSeed
    {
        public static async Task SeedAsync(
           UserManager<ApplicationUser> userManager,
           RoleManager<ApplicationRole> roleManager)
        {
            foreach (var role in EnumHelper<Roles>.GetEnumValues())
            {
                var roleName = role.ToString();
                if (!await roleManager.RoleExistsAsync(roleName))
                    await roleManager.CreateAsync(new ApplicationRole(roleName));
            }
        }
    }
}
