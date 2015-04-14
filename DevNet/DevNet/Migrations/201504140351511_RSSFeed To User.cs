namespace DevNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RSSFeedToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "RssFeedName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "RssFeedName");
        }
    }
}
