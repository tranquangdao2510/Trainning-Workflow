namespace MultipleDbContextDemo.MigrationsSecond
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class add_product : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    Active = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Product",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ProductName = c.String(),
                    Quantity = c.Int(nullable: false),
                    Active = c.Boolean(nullable: false),
                    CategoryId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Product", "CategoryId", "dbo.Category");
            DropIndex("dbo.Product", new[] { "CategoryId" });
            DropTable("dbo.Product");
            DropTable("dbo.Category");
        }
    }
}