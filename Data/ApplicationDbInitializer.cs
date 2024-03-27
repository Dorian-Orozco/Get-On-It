using GetOnIt.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GetOnIt.Data
{
    public static class ApplicationDbInitializer
    {
        public static async void Seed(IApplicationBuilder applicationBuilder)
        {
            ApplicationDbContext context = applicationBuilder.ApplicationServices.CreateScope()
                .ServiceProvider.GetRequiredService<ApplicationDbContext>();
            try
            {
                //Create database if it doesn't exist/apply migrations
                context.Database.Migrate();

                //Could implement roles in future date? Maybe have people be able to create a section together?

                //Adding demo users to not have to login all the time
                var userManager = applicationBuilder.ApplicationServices.CreateScope()
                    .ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                if (userManager.FindByEmailAsync("dorozco4@ncstudents.niagaracollege.ca").Result == null) //So if my account does not exist, create it
                {
                    ApplicationUser user = new ApplicationUser
                    {
                        FirstName = "Dorian",
                        LastName = "Orozco",
                        UserName = "dorozco4@ncstudents.niagaracollege.ca",
                        Email = "dorozco4@ncstudents.niagaracollege.ca",
                        EmailConfirmed = true
                    };
                    IdentityResult result = userManager.CreateAsync(user, "password").Result; //once you put in password settings, if these don't align, it will fail!
                    
                }
                if (userManager.FindByEmailAsync("admin@outlook.com").Result == null)
                {
                    ApplicationUser user = new ApplicationUser
                    {
                        FirstName = "Admin",
                        LastName = "Powers",
                        UserName = "admin@outlook.com",
                        Email = "admin@outlook.com",
                        EmailConfirmed = true
                    };
                    IdentityResult result = userManager.CreateAsync(user, "password").Result; //! 
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.GetBaseException().Message);
            }
        }
    }
}
