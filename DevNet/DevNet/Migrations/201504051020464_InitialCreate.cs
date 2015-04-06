namespace DevNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FavoriteIDEs",
                c => new
                    {
                        FavoriteIDEID = c.Int(nullable: false, identity: true),
                        FavoriteIDEName = c.String(),
                    })
                .PrimaryKey(t => t.FavoriteIDEID);
            
            CreateTable(
                "dbo.ProgrammingLanguages",
                c => new
                    {
                        ProgrammingLanguageID = c.Int(nullable: false, identity: true),
                        ProgrammingLanguageName = c.String(),
                    })
                .PrimaryKey(t => t.ProgrammingLanguageID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.SoftwareSpecialties",
                c => new
                    {
                        SoftwareSpecialtyID = c.Int(nullable: false, identity: true),
                        SoftwareSpecialtyName = c.String(),
                    })
                .PrimaryKey(t => t.SoftwareSpecialtyID);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        StateID = c.Int(nullable: false, identity: true),
                        StateName = c.String(),
                        StateAbbreviation = c.String(),
                    })
                .PrimaryKey(t => t.StateID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        StateID = c.Int(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        FavoriteIDEID = c.Int(nullable: false),
                        SoftwareSpecialtyID = c.Int(nullable: false),
                        ProgrammingLanguageID = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FavoriteIDEs", t => t.FavoriteIDEID, cascadeDelete: true)
                .ForeignKey("dbo.ProgrammingLanguages", t => t.ProgrammingLanguageID, cascadeDelete: true)
                .ForeignKey("dbo.SoftwareSpecialties", t => t.SoftwareSpecialtyID, cascadeDelete: true)
                .ForeignKey("dbo.States", t => t.StateID, cascadeDelete: true)
                .Index(t => t.StateID)
                .Index(t => t.FavoriteIDEID)
                .Index(t => t.SoftwareSpecialtyID)
                .Index(t => t.ProgrammingLanguageID)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "StateID", "dbo.States");
            DropForeignKey("dbo.AspNetUsers", "SoftwareSpecialtyID", "dbo.SoftwareSpecialties");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "ProgrammingLanguageID", "dbo.ProgrammingLanguages");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "FavoriteIDEID", "dbo.FavoriteIDEs");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "ProgrammingLanguageID" });
            DropIndex("dbo.AspNetUsers", new[] { "SoftwareSpecialtyID" });
            DropIndex("dbo.AspNetUsers", new[] { "FavoriteIDEID" });
            DropIndex("dbo.AspNetUsers", new[] { "StateID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.States");
            DropTable("dbo.SoftwareSpecialties");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ProgrammingLanguages");
            DropTable("dbo.FavoriteIDEs");
        }
    }
}
