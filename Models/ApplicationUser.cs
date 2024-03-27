using Microsoft.AspNetCore.Identity;
namespace GetOnIt.Models
{
    //https://stackoverflow.com/questions/73131275/how-to-add-first-last-name-properties-to-identity-user-in-asp-net-core-mvc
    public class ApplicationUser : IdentityUser //Inherit from the Identity user system to access specific information about the user
    {
        public string FirstName { get; set; }
        public string LastName { get; set; } 
    }
}

//To learn more about customizing user authenthication 
//https://learn.microsoft.com/en-us/aspnet/core/security/authentication/customize-identity-model?view=aspnetcore-6.0