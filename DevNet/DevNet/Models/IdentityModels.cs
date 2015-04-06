using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevNet.Models
{
   
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public String FirstName { get; set; } 
        public String LastName { get; set; } 
        public String Address { get; set; } 
        public String City { get; set; }
        public Int32 StateID { get; set; } 
        public DateTime DateOfBirth { get; set; } 
        public Int32 FavoriteIDEID { get; set; } 
        public Int32 SoftwareSpecialtyID { get; set; } 
        public Int32 ProgrammingLanguageID { get; set; }

        public virtual State State { get; set; }
        public virtual FavoriteIDE FavoriteIDE { get; set; }
        public virtual SoftwareSpecialty SoftwareSpecialty { get; set; }
        public virtual ProgrammingLanguage ProgrammingLanguage { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<State> States { get; set; }
        public DbSet<FavoriteIDE> FavoriteIDEs { get; set; }
        public DbSet<SoftwareSpecialty> SoftwareSpecialties { get; set; }
        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}