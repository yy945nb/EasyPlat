namespace EasyPlat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePhidDataType : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.BeginProjectScaleInfo");
            DropPrimaryKey("dbo.WorkProjectScaleInfo");
            AlterColumn("dbo.BeginProjectScaleInfo", "Phid", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.BeginProjectScaleInfo", "PPhid", c => c.Long(nullable: false));
            AlterColumn("dbo.WorkProjectScaleInfo", "Phid", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.WorkProjectScaleInfo", "PPhid", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.BeginProjectScaleInfo", "Phid");
            AddPrimaryKey("dbo.WorkProjectScaleInfo", "Phid");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.WorkProjectScaleInfo");
            DropPrimaryKey("dbo.BeginProjectScaleInfo");
            AlterColumn("dbo.WorkProjectScaleInfo", "PPhid", c => c.Int(nullable: false));
            AlterColumn("dbo.WorkProjectScaleInfo", "Phid", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.BeginProjectScaleInfo", "PPhid", c => c.Int(nullable: false));
            AlterColumn("dbo.BeginProjectScaleInfo", "Phid", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.WorkProjectScaleInfo", "Phid");
            AddPrimaryKey("dbo.BeginProjectScaleInfo", "Phid");
        }
    }
}
