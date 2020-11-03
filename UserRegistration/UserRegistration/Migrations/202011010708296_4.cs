namespace WebApplication4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4 : DbMigration
    {
        public override void Up()
        {
            RenameIndex(table: "dbo.Users", name: "Email", newName: "Ix_Email");
            RenameIndex(table: "dbo.Users", name: "Phone", newName: "Ix_Phone");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Users", name: "Ix_Phone", newName: "Phone");
            RenameIndex(table: "dbo.Users", name: "Ix_Email", newName: "Email");
        }
    }
}
