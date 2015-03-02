using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DevNet.Models
{
    public class DeveloperInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var developers = new List<Developer>
            {
                new Developer
                {
                    FirstName = "Debra",
                    LastName = "Garcia",
                    Address = "1234 Main St",
                    City = "Redmond",
                    StateID = 35,
                    Zip = "10999",
                    Email = "debra@example.com",
                    FavoriteLanguage = "C#"
                },
                new Developer
                {
                    FirstName = "Thorsten",
                    LastName = "Weinrich",
                    Address = "5678 1st Ave W",
                    City = "Redmond",
                    StateID = 35,
                    Zip = "10999",
                    Email = "thorsten@example.com",
                    FavoriteLanguage = "C#"
                },
                new Developer
                {
                    FirstName = "Yuhong",
                    LastName = "Li",
                    Address = "9012 State st",
                    City = "Redmond",
                    StateID = 35,
                    Zip = "10999",
                    Email = "yuhong@example.com",
                    FavoriteLanguage = "C++"
                },
                new Developer
                {
                    FirstName = "Jon",
                    LastName = "Orton",
                    Address = "3456 Maple St",
                    City = "Redmond",
                    StateID = 35,
                    Zip = "10999",
                    Email = "jon@example.com",
                    FavoriteLanguage = "C++"
                },
                new Developer
                {
                    FirstName = "Diliana",
                    LastName = "Alexieva-Bosseva",
                    Address = "7890 2nd Ave E",
                    City = "Redmond",
                    StateID = 35,
                    Zip = "10999",
                    Email = "diliana@example.com",
                    FavoriteLanguage = "Java"
                }
             
            };
            developers.ForEach(s => context.Developers.Add(s));
            context.SaveChanges();
        }
    }
}