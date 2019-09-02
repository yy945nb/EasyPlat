using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EasyPlat.Models
{
    public class ProjectPlanInfo
    {
        [Key]
        public long Phid { get; set; }

        [Display(Name = "序号")]
        public long LineId { get; set; }

        [Display(Name = "项目编码")]
        public string ProjectNo { get; set; }

        [Display(Name = "省公司")]
        public string Province { get; set; }

        [Display(Name = "建设管理单位")]
        public string Company { get; set; }

        [Display(Name = "电压等级")]
        public string CLevel { get; set; }

        [Display(Name = "项目名称")]
        public string ProjectName { get; set; }

        [Display(Name = "项目建设类别")]
        public string Catagory { get; set; }

        [Display(Name = "单项工程类型")]
        public string PType { get; set; }

        [Display(Name = "建设性质")]
        public string Nature { get; set; }

        [Display(Name = "计划性质-开工")]
        public string IsBegin { get; set; }

        [Display(Name = "")]
        public string IsWork { get; set; }

        [Display(Name = "计划规模-线路长度")]
        public string Vlength { get; set; }

        [Display(Name = "计划规模-变电容量")]
        public string Volume { get; set; }

        [Display(Name = "实物量-线路回数")]
        public string CycleNum { get; set; }

        [Display(Name = "实物量-主变组数")]
        public string GroupNum { get; set; }

        [Display(Name = "项目所在地区")]
        public string Address { get; set; }

        [Display(Name = "项目前期工作-可研批复文号")]
        public string Pre_fno { get; set; }

        [Display(Name = "项目前期工作-可研批复时间")]
        public DateTime? Pre_bdt { get; set; }

        [Display(Name = "项目前期工作-用地预审文号")]
        public string Pre_yno { get; set; }

        [Display(Name = "项目前期工作-选址意见书文号")]
        public string Pre_sno { get; set; }

        [Display(Name = "项目前期工作-用地指标是否落实")]
        public string Pre_isable { get; set; }

        [Display(Name = "项目前期工作-核准文号")]
        public string Pre_hno { get; set; }

        [Display(Name = "项目前期工作-核准批复时间")]
        public DateTime? Pre_hdt { get; set; }

        [Display(Name = "项目前期工作-环评批复时间")]
        public DateTime? Pre_pdt { get; set; }

        [Display(Name = "项目前期工作-水保批复时间")]
        public DateTime? Pre_sdt { get; set; }

        [Display(Name = "工程前期阶段-设计招标计划申报截止时间")]
        public DateTime? Start_zdt { get; set; }

        [Display(Name = "工程前期阶段-设计招标开标时间")]
        public DateTime? Start_kdt { get; set; }

        [Display(Name = "工程前期阶段-初设评审时间")]
        public DateTime? Start_sdt { get; set; }

        [Display(Name = "工程前期阶段-初设批复时间")]
        public DateTime? Start_fdt { get; set; }

        [Display(Name = "工程前期阶段-施工图预算批复时间")]
        public DateTime? Start_ydt { get; set; }

        [Display(Name = "工程前期阶段-物资招标计划申报截止时间")]
        public DateTime? Start_wdt { get; set; }

        [Display(Name = "工程前期阶段-物质招标开标时间")]
        public DateTime? Start_bdt { get; set; }

        [Display(Name = "工程前期阶段-施工招标申报截止时间")]
        public DateTime? Start_jdt { get; set; }

        [Display(Name = "工程前期阶段-施工招标开标时间")]
        public DateTime? Start_gdt { get; set; }

        [Display(Name = "工程前期阶段-建设用地规划许可证取得时间")]
        public DateTime? Start_xdt { get; set; }

        [Display(Name = "工程前期阶段-路径规划批复")]
        public DateTime? Start_ldt { get; set; }

        [Display(Name = "工程前期阶段-建设用地批准书取得时间")]
        public DateTime? Start_pdt { get; set; }

        [Display(Name = "工程前期阶段-国家土地使用不动产权证取得时间")]
        public DateTime? Start_cdt { get; set; }

        [Display(Name = "工程前期阶段-建设工程规划许可证取得时间")]
        public DateTime? Start_hdt { get; set; }

        [Display(Name = "工程前期阶段-变电站消防设计审核时间")]
        public DateTime? Start_ddt { get; set; }

        [Display(Name = "工程前期阶段-线路复测开始时间")]
        public DateTime? Start_rdt { get; set; }

        [Display(Name = "工程建设阶段-开工时间")]
        public DateTime? Build_bdt { get; set; }

        [Display(Name = "工程建设阶段-安装组他开始时间")]
        public DateTime? Build_sdt { get; set; }

        [Display(Name = "工程建设阶段-调试架线时间")]
        public DateTime? Build_xdt { get; set; }

        [Display(Name = "工程建设阶段-消防验收时间")]
        public DateTime? Build_ydt { get; set; }

        [Display(Name = "工程建设阶段-环保验收完成时间")]
        public DateTime? Build_hdt { get; set; }

        [Display(Name = "工程建设阶段-水保验收完成时间")]
        public DateTime? Build_cdt { get; set; }

        [Display(Name = "工程建设阶段-投产时间")]
        public DateTime? Build_tdt { get; set; }

        [Display(Name = "总结评价阶段-竣工时间")]
        public DateTime? Finish_jdt { get; set; }

        [Display(Name = "总结评价阶段-竣工阶段批复时间")]
        public DateTime? Finish_fdt { get; set; }

        [Display(Name = "总结评价阶段-创优自检完成时间")]
        public DateTime? Finish_zdt { get; set; }

        [Display(Name = "可研批复动态投资（万）")]
        public decimal DynamicNum { get; set; }
        
        [Display(Name = "年度预算需求计划（万）")]
        public decimal DemandNum { get; set; }

        [Display(Name = "开工至年底累计完成投资（万）")]
        public decimal TotalNum { get; set; }

        [Display(Name = "下年度投资计划（万）")]
        public decimal PlanNum { get; set; }

        [Display(Name = "年底单项工程完成进度")]
        public string Progress { get; set; }

        [Display(Name = "备注")]
        public string Remark { get; set; }

        [Display(Name = "重点工程-特高配套")]
        public string Important_high { get; set; }

        [Display(Name = "重点工程-500/220配套")]
        public string Important_assort { get; set; }

        [Display(Name = "重点工程-电网加强")]
        public string Important_strong { get; set; }

        [Display(Name = "重点工程-电铁配套")]
        public string Important_iron { get; set; }

        [Display(Name = "重点工程-跨越铁路")]
        public string Important_rail { get; set; }

        [Display(Name = "重点工程-风电送出")]
        public string Important_wind { get; set; }

        [Display(Name = "重点工程-光伏送出")]
        public string Important_light { get; set; }

        [Display(Name = "重点工程-电厂送出")]
        public string Important_give { get; set; }

        [Display(Name = "重点工程-城农网专项")]
        public string Important_farmer { get; set; }

        [Display(Name = "重点工程-迎峰度夏")]
        public string Important_summary { get; set; }

        [Display(Name = "工期月数")]
        public string Months { get; set; }

        [Display(Name = "开工准备")]
        public string WorkReady { get; set; }

        [Display(Name = "可研初设")]
        public string WorkSet { get; set; }

        [Display(Name = "开工初设")]
        public string WorkStart { get; set; }

        [Display(Name = "开工招标")]
        public string WorkBid { get; set; }

        [Display(Name = "开工核准")]
        public string WorkCheck { get; set; }

    }
}