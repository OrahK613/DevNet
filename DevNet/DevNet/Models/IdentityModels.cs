using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ServiceModel.Syndication;
using System.Collections.Generic;

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
        public String RssFeedName { get; set; }

       

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

        // Concatenate the address info for display in tables and such:
        public string DisplayAddress
        {
           get
           {
                string dspAddress = string.IsNullOrWhiteSpace(this.Address) ? "" : this.Address;
                string dspCity = string.IsNullOrWhiteSpace(this.City) ? "" : this.City;
                string dspState = "";
                if (this.State != null)
                {
                  
                    dspState = string.IsNullOrWhiteSpace(this.State.StateAbbreviation) ? "" : this.State.StateAbbreviation;
                }
                else
                {
                    dspState = "some state";
                }
               
                return string.Format("{0} {1} {2}", dspAddress, dspCity, dspState );
            }
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public System.Data.Entity.DbSet<State> States { get; set; }
        public System.Data.Entity.DbSet<FavoriteIDE> FavoriteIDEs { get; set; }
        public System.Data.Entity.DbSet<SoftwareSpecialty> SoftwareSpecialties { get; set; }
        public System.Data.Entity.DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
       

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        static ApplicationDbContext()
        {
            // Set the database intializer which is run once during application start
            // This seeds the database with admin user credentials and admin role
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //public System.Data.Entity.DbSet<DevNet.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}