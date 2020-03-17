namespace Our_Ecommerce_store_GIS_40.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_order_detail_table : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderDetails", "Order_OrderID", "dbo.Orders");
            DropIndex("dbo.OrderDetails", new[] { "Order_OrderID" });
            RenameColumn(table: "dbo.OrderDetails", name: "Order_OrderID", newName: "FK_order");
            AlterColumn("dbo.OrderDetails", "FK_order", c => c.Int(nullable: false));
            CreateIndex("dbo.OrderDetails", "FK_order");
            AddForeignKey("dbo.OrderDetails", "FK_order", "dbo.Orders", "OrderID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "FK_order", "dbo.Orders");
            DropIndex("dbo.OrderDetails", new[] { "FK_order" });
            AlterColumn("dbo.OrderDetails", "FK_order", c => c.Int());
            RenameColumn(table: "dbo.OrderDetails", name: "FK_order", newName: "Order_OrderID");
            CreateIndex("dbo.OrderDetails", "Order_OrderID");
            AddForeignKey("dbo.OrderDetails", "Order_OrderID", "dbo.Orders", "OrderID");
        }
    }
}
