namespace WebApplication4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            RenameIndex(table: "dbo.Users", name: "IX_Email", newName: "Email");
            RenameIndex(table: "dbo.Users", name: "IX_Phone", newName: "Phone");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Users", name: "Phone", newName: "IX_Phone");
            RenameIndex(table: "dbo.Users", name: "Email", newName: "IX_Email");
        }
    }
}
