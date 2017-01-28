namespace Web_Dev3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTodos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TodoModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titel = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Owner_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Owner_Id)
                .Index(t => t.Owner_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TodoModels", "Owner_Id", "dbo.AspNetUsers");
            DropIndex("dbo.TodoModels", new[] { "Owner_Id" });
            DropTable("dbo.TodoModels");
        }
    }
}
