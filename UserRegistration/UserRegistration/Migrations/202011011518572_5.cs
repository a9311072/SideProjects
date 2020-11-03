namespace WebApplication4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _5 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Users", "Ix_Email");
            DropIndex("dbo.Users", "Ix_Phone");
            DropColumn("dbo.Users", "ConfirmPassword");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "ConfirmPassword", c => c.String(nullable: false));
            CreateIndex("dbo.Users", "Phone", unique: true, name: "Ix_Phone");
            CreateIndex("dbo.Users", "Email", unique: true, name: "Ix_Email");
        }
    }
}
