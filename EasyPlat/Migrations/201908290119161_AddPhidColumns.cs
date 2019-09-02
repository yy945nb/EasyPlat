namespace EasyPlat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPhidColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BeginProjectScaleInfo", "PPhid", c => c.Int(nullable: false));
            AddColumn("dbo.WorkProjectScaleInfo", "PPhid", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkProjectScaleInfo", "PPhid");
            DropColumn("dbo.BeginProjectScaleInfo", "PPhid");
        }
    }
}
