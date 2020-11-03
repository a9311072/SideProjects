namespace WebApplication4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Users", "Email");
            AlterColumn("dbo.Users", "Email", c => c.String(maxLength: 128));
            CreateIndex("dbo.Users", "Email", unique: true, name: "Email");
            DropColumn("dbo.Users", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            DropIndex("dbo.Users", "Email");
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Users", "Email", unique: true, name: "Email");
        }
    }
}
