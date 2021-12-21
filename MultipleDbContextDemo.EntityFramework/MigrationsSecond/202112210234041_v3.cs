namespace MultipleDbContextDemo.MigrationsSecond
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Product", "Quantity", c => c.Int());
            AlterColumn("dbo.Product", "Active", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Product", "Active", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Product", "Quantity", c => c.Int(nullable: false));
        }
    }
}
