namespace DevasolVidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRentalQuantityToMovie : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "RentalQuantityAvailable", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "RentalQuantityAvailable");
        }
    }
}
