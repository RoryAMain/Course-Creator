using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

namespace WebDevProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime Birthdate { get; set; }
        public string FavoriteToy { get; set; }
    }
}
