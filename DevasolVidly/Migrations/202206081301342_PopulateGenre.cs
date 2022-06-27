namespace DevasolVidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenre : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres (Name) VALUES ('Highlife')");
            Sql("INSERT INTO Genres (Name) VALUES ('Amapiano')");
            Sql("INSERT INTO Genres (Name) VALUES ('Afrobeats')");
            Sql("INSERT INTO Genres (Name) VALUES ('Rap')");
            Sql("INSERT INTO Genres (Name) VALUES ('Gospel')");
        }
        
        public override void Down()
        {
        }
    }
}
