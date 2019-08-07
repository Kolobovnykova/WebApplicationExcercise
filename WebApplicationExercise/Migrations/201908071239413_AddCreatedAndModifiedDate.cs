namespace WebApplicationExercise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreatedAndModifiedDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Products", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Products", "ModifiedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ModifiedDate");
            DropColumn("dbo.Products", "CreatedDate");
            DropColumn("dbo.Orders", "ModifiedDate");
        }
    }
}
