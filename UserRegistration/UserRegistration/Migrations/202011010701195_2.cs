namespace WebApplication4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Users", "Email");
            AddColumn("dbo.Users", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Users", "Email", unique: true, name: "Email");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", "Email");
            AlterColumn("dbo.Users", "Email", c => c.String(maxLength: 128));
            DropColumn("dbo.Users", "Discriminator");
            CreateIndex("dbo.Users", "Email", unique: true, name: "Email");
        }
    }
}
