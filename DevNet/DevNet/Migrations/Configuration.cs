using DevNet.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DevNet.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DevNet.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DevNet.Models.ApplicationDbContext context)
        {
            AddUserAndRole(context);
            context.Developers.AddOrUpdate(p => new { p.LastName, p.FirstName },
            new Developer
            {
                FirstName = "Debra",
                LastName = "Garcia",
                Address = "1234 Main St",
                City = "Redmond",
                StateID = 35,
                Zip = "10999",
                Email = "debra@example.com",
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
             }
             );
        }

        bool AddUserAndRole(DevNet.Models.ApplicationDbContext context)
        {
            IdentityResult ir;
            var rm = new RoleManager<IdentityRole>
                (new RoleStore<IdentityRole>(context));
            ir = rm.Create(new IdentityRole("canEdit"));
            var um = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser()
            {
                UserName = "user1@contoso.com",
            };
            ir = um.Create(user, "P_assw0rd1");
            if (ir.Succeeded == false)
                return ir.Succeeded;
            ir = um.AddToRole(user.Id, "canEdit");
            return ir.Succeeded;
        }
    }
}
