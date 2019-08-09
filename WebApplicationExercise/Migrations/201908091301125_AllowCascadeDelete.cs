namespace WebApplicationExercise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AllowCascadeDelete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "Order_Id", "dbo.Orders");
            AddForeignKey("dbo.Products", "Order_Id", "dbo.Orders", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Order_Id", "dbo.Orders");
            AddForeignKey("dbo.Products", "Order_Id", "dbo.Orders", "Id");
        }
    }
}
