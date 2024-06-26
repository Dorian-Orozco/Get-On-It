﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
namespace GetOnIt.Models
{
    //https://stackoverflow.com/questions/73131275/how-to-add-first-last-name-properties-to-identity-user-in-asp-net-core-mvc
    public class ApplicationUser : IdentityUser //Inherit from the Identity user system to access specific information about the user
    {
        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; } 
    }
}

//To learn more about customizing user authenthication 
//https://learn.microsoft.com/en-us/aspnet/core/security/authentication/customize-identity-model?view=aspnetcore-6.0