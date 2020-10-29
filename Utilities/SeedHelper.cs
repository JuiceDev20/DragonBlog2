using DragonBlog2.Data;
using DragonBlog2.Enum;
using DragonBlog2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonBlog2.Utilities
{
    public static class SeedHelper
    {
        public static async Task SeedDataAsync(UserManager<BlogUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await SeedRoles(roleManager);
            await SeedAdmin(userManager);
            await SeedModerator(userManager);
        }

        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Moderator.ToString()));
        }

        private static async Task SeedAdmin(UserManager<BlogUser> userManager)
        {
            if (await userManager.FindByEmailAsync("ojolmo@gmail.com") == null)
            {
                var admin = new BlogUser()
                {
                    Email = "ojolmo@gmail.com",
                    UserName = "ojolmo@gmail.com",
                    FirstName = "Orlando",
                    LastName = "Olmo",
                    EmailConfirmed = true,
                };
                await userManager.CreateAsync(admin, "Abc123!");
                await userManager.AddToRoleAsync(admin, Roles.Admin.ToString());
            }
        }

        private static async Task SeedModerator(UserManager<BlogUser> userManager)
        {
            if (await userManager.FindByEmailAsync("ojolmo8@gmail.com") == null)
            {
                var admin = new BlogUser()
                {
                    Email = "ojolmo8@gmail.com",
                    UserName = "ojolmo8@gmail.com",
                    FirstName = "Orlando",
                    LastName = "Olmo",
                    EmailConfirmed = true,
                };
                await userManager.CreateAsync(admin, "Abc123!");
                await userManager.AddToRoleAsync(admin, Roles.Moderator.ToString());
            }
        }

    }
}
