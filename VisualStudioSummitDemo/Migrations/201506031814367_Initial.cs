namespace VisualStudioSummitDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactDealer",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        ContactId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contact", t => t.ContactId, cascadeDelete: true)
                .Index(t => t.ContactId);
            
            CreateTable(
                "dbo.Contact",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Genre = c.Int(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        Inactive = c.Boolean(nullable: false),
                        TenantId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tenant", t => t.TenantId, cascadeDelete: true)
                .Index(t => t.TenantId);
            
            CreateTable(
                "dbo.Tenant",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContactDealer", "ContactId", "dbo.Contact");
            DropForeignKey("dbo.Contact", "TenantId", "dbo.Tenant");
            DropIndex("dbo.Contact", new[] { "TenantId" });
            DropIndex("dbo.ContactDealer", new[] { "ContactId" });
            DropTable("dbo.Tenant");
            DropTable("dbo.Contact");
            DropTable("dbo.ContactDealer");
        }
    }
}
