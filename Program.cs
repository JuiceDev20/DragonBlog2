using System;
using System.Threading.Tasks;
using DragonBlog2.Models;
using DragonBlog2.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DragonBlog2
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            await DataHelper.ManageData(host);
            await SeedDataAsync(host);
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.CaptureStartupErrors(true);
                    webBuilder.UseSetting(WebHostDefaults.DetailedErrorsKey, "true");
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
