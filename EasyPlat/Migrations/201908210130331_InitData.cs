namespace EasyPlat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitData : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BeginProjectScaleInfo",
                c => new
                    {
                        Phid = c.Int(nullable: false, identity: true),
                        Year = c.String(),
                        Month = c.String(),
                        VolLevel = c.String(),
                        PNumber = c.Int(nullable: false),
                        CircuitNum = c.Int(nullable: false),
                        TableNum = c.Int(nullable: false),
                        Length = c.Double(nullable: false),
                        Volume = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Phid);
            
            CreateTable(
                "dbo.PlanBatchInfo",
                c => new
                    {
                        Phid = c.Long(nullable: false, identity: true),
                        PlanBatch = c.String(),
                        PlanName = c.String(),
                        CityPlace = c.String(),
                        BidMode = c.String(),
                        BuyWay = c.String(),
                        BidYear = c.String(),
                        BidFlag = c.String(),
                        PlanCheckDt = c.DateTime(),
                        PlanEndDt = c.DateTime(),
                        Status = c.String(),
                        PlanStartDt = c.DateTime(),
                        CityStartDt = c.DateTime(),
                        Period = c.String(),
                    })
                .PrimaryKey(t => t.Phid);
            
            CreateTable(
                "dbo.ProjectPlanInfo",
                c => new
                    {
                        Phid = c.Long(nullable: false, identity: true),
                        LineId = c.Long(nullable: false),
                        ProjectNo = c.String(),
                        Province = c.String(),
                        Company = c.String(),
                        CLevel = c.String(),
                        ProjectName = c.String(),
                        Catagory = c.String(),
                        PType = c.String(),
                        Nature = c.String(),
                        IsBegin = c.String(),
                        IsWork = c.String(),
                        Vlength = c.String(),
                        Volume = c.String(),
                        CycleNum = c.String(),
                        GroupNum = c.String(),
                        Address = c.String(),
                        Pre_fno = c.String(),
                        Pre_bdt = c.DateTime(),
                        Pre_yno = c.String(),
                        Pre_sno = c.String(),
                        Pre_isable = c.String(),
                        Pre_hno = c.String(),
                        Pre_hdt = c.DateTime(),
                        Pre_pdt = c.DateTime(),
                        Pre_sdt = c.DateTime(),
                        Start_zdt = c.DateTime(),
                        Start_kdt = c.DateTime(),
                        Start_sdt = c.DateTime(),
                        Start_fdt = c.DateTime(),
                        Start_ydt = c.DateTime(),
                        Start_wdt = c.DateTime(),
                        Start_bdt = c.DateTime(),
                        Start_jdt = c.DateTime(),
                        Start_gdt = c.DateTime(),
                        Start_xdt = c.DateTime(),
                        Start_ldt = c.DateTime(),
                        Start_pdt = c.DateTime(),
                        Start_cdt = c.DateTime(),
                        Start_hdt = c.DateTime(),
                        Start_ddt = c.DateTime(),
                        Start_rdt = c.DateTime(),
                        Build_bdt = c.DateTime(),
                        Build_sdt = c.DateTime(),
                        Build_xdt = c.DateTime(),
                        Build_ydt = c.DateTime(),
                        Build_hdt = c.DateTime(),
                        Build_cdt = c.DateTime(),
                        Build_tdt = c.DateTime(),
                        Finish_jdt = c.DateTime(),
                        Finish_fdt = c.DateTime(),
                        Finish_zdt = c.DateTime(),
                        DynamicNum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DemandNum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalNum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PlanNum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Progress = c.String(),
                        Remark = c.String(),
                        Important_high = c.String(),
                        Important_assort = c.String(),
                        Important_strong = c.String(),
                        Important_iron = c.String(),
                        Important_rail = c.String(),
                        Important_wind = c.String(),
                        Important_light = c.String(),
                        Important_give = c.String(),
                        Important_farmer = c.String(),
                        Important_summary = c.String(),
                        Months = c.String(),
                        WorkReady = c.String(),
                        WorkSet = c.String(),
                        WorkStart = c.String(),
                        WorkBid = c.String(),
                        WorkCheck = c.String(),
                    })
                .PrimaryKey(t => t.Phid);
            
            CreateTable(
                "dbo.WorkProjectScaleInfo",
                c => new
                    {
                        Phid = c.Int(nullable: false, identity: true),
                        Year = c.String(),
                        Month = c.String(),
                        VolLevel = c.String(),
                        PNumber = c.Int(nullable: false),
                        CircuitNum = c.Int(nullable: false),
                        TableNum = c.Int(nullable: false),
                        Length = c.Double(nullable: false),
                        Volume = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Phid);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WorkProjectScaleInfo");
            DropTable("dbo.ProjectPlanInfo");
            DropTable("dbo.PlanBatchInfo");
            DropTable("dbo.BeginProjectScaleInfo");
        }
    }
}
