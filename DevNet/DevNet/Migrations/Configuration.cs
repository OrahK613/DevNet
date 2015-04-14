namespace DevNet.Migrations
{
    using DevNet.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using Microsoft.Owin.Security;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web;

    internal sealed class Configuration : DbMigrationsConfiguration<DevNet.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "DevNet.Models.ApplicationDbContext";
        }

        protected override void Seed(DevNet.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //var states = new List<State>
            //{
            //    new State { StateID = 1,   StateName = "Ohio", StateAbbreviation = "OH" },
            //    new State { StateID = 2,   StateName = "Kentucky",  StateAbbreviation = "KY" },
            //    new State { StateID = 3,   StateName = "California", StateAbbreviation = "CA" },    
            //};
            //states.ForEach(s => context.States.AddOrUpdate(p => p.StateID, s));
            //context.SaveChanges();

            //var favoriteIDEs = new List<FavoriteIDE>
            //{
            //    new FavoriteIDE { FavoriteIDEID = 1,   FavoriteIDEName = "Visual Studio"},
            //    new FavoriteIDE { FavoriteIDEID = 2,   FavoriteIDEName = "Eclipse"},
            //    new FavoriteIDE { FavoriteIDEID = 3,   FavoriteIDEName = "Qt"},    
            //};
            //favoriteIDEs.ForEach(s => context.FavoriteIDEs.AddOrUpdate(p => p.FavoriteIDEID, s));
            //context.SaveChanges();

            //var softwareSpecialties = new List<SoftwareSpecialty>
            //{
            //    new SoftwareSpecialty { SoftwareSpecialtyID = 1,   SoftwareSpecialtyName = ".NET Developer"},
            //    new SoftwareSpecialty { SoftwareSpecialtyID = 2,   SoftwareSpecialtyName = "Java Developer"},
            //    new SoftwareSpecialty { SoftwareSpecialtyID = 3,   SoftwareSpecialtyName = "C++ Developer"},    
            //};
            //softwareSpecialties.ForEach(s => context.SoftwareSpecialties.AddOrUpdate(p => p.SoftwareSpecialtyID, s));
            //context.SaveChanges();

            //var programmingLanguages = new List<ProgrammingLanguage>
            //{
            //    new ProgrammingLanguage { ProgrammingLanguageID = 1,   ProgrammingLanguageName = "C#"},
            //    new ProgrammingLanguage { ProgrammingLanguageID = 2,   ProgrammingLanguageName = "Java"},
            //    new ProgrammingLanguage { ProgrammingLanguageID = 3,   ProgrammingLanguageName = "C++"},    
            //};
            //programmingLanguages.ForEach(s => context.ProgrammingLanguages.AddOrUpdate(p => p.ProgrammingLanguageID, s));
            //context.SaveChanges();
        }



    }
}
