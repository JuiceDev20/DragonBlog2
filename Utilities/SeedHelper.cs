using DragonBlog2.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonBlog2.Utilities
{
    public class SeedHelper
    {
        private ApplicationDbContext _context;
        
        public SeedHelper(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedData()
        {
            SeedRoles();
            SeedUsersAndAssign();
        }

        public async void SeedRoles()
        {
            var roleStore = new RoleStore<IdentityRole>(_context);

            var roles = new List<string> { "Admin", "Moderator" };
            foreach(var role in roles)
            {
                if(!_context.Roles.Any(r => r.Name == "admin"))
                {
                    await roleStore.CreateAsync(new IdentityRole { Name = role, NormalizedName = role.ToLower() });
                }
            }
        }
    
    public async void SeedAdminUser()
    {

    }
}
