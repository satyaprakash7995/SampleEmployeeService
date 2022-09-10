using SampleEmployeeService.Domain.Constants;
using SampleEmployeeService.Domain.Enums;
using SampleEmployeeService.Domain.Helpers;
using SampleEmployeeService.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SampleEmployeeService.Infrastructure.Identity.Seeds
{
    public static class DefaultAdminUserSeed
    {
        public static async Task SeedAsync(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "anthonytravelportaladmin@localhost.com",
                Email = "anthonytravelportaladmin@localhost.com",
                FirstName = "System",
                LastName = "Admin",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true
            };

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "2022Super@Pa$$word!");

                    foreach (var role in EnumHelper<Roles>.GetEnumValues())
                        await userManager.AddToRoleAsync(defaultUser, role.ToString());
                }

                await roleManager.SeedClaimsForAdmin();
            }
        }

        private static async Task SeedClaimsForAdmin(this RoleManager<ApplicationRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync("Admin");
            await roleManager.AddPermissionClaim(adminRole, "Users");
        }

        private static async Task AddPermissionClaim(
            this RoleManager<ApplicationRole> roleManager,
            ApplicationRole role,
            string module)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissions = PermissionsGenerator.GeneratePermissionsForModule(module);

            foreach (var permission in allPermissions)
                if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
                    await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, permission));
        }
    }
}
