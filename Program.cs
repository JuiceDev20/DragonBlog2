using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DragonBlog2.Data;
using DragonBlog2.Models;
using DragonBlog2.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DragonBlog2
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            await SeedDataAsync(host);
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public async static Task SeedDataAsync(IHost host) 
        {
            using (var scope = host.Services.CreateScope())
            {
            var services = scope.ServiceProvider;
            try 
            {
                var userManager = services.GetRequiredService<UserManager<BlogUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                await SeedHelper.SeedDataAsync(userManager, roleManager);
                }
                catch(Exception ex)
                {
                  Console.WriteLine(ex);
                }   
           }
       }
   }
}
